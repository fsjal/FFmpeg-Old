using FFmpeg.Entities;
using FFmpeg.Model.Codecs;
using FFmpeg.Util;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace FFmpeg.Model
{
    public static class FFmpegConverter
    {
        private static ILog log = Logger.Log;

        public static async Task ConvertAsync(Media media, List<IParam> parameters, CancellationToken token)
        {
            string newFile = NewFile(media, parameters);
            string paramsStr = string.Join(" ", parameters.Select(e => $"{e.Key} {e.Value}"));
            string commandStr = $"-i \"{media.Path}\" {paramsStr} \"{newFile}\"";
            ProcessCommand process = new ProcessCommand(commandStr, token);
            int duration = await GetDurationAsync(media, parameters, token);

            log.Info(duration);
            media.State = State.Converting;
            await foreach (string result in process.ResultAsync(token))
            {
                ProcessResult(media, result, duration);
            }
            media.Eta = "Finished";
            media.State = State.Finished;
            log.Info($"{media.FileName} finished");
        }

        public static async Task<int> GetDurationAsync(Media media, List<IParam> parameters, CancellationToken token)
        {
            int duration = 0;
            IParam startTime = parameters.Find(e => e.Key == "-ss");
            IParam endTime = parameters.Find(e => e.Key == "-to");
            int[] times = new int[] { 3600, 60, 1 };
            string commandStr = $"-i \"{media.Path}\"";
            ProcessCommand process = new ProcessCommand(commandStr, token);

            await foreach (string result in process.ResultAsync(token))
            {
                log.Info(result);
                if (result != null && result.Contains("Duration"))
                {
                    Match regex = new Regex(@"Duration: (\d+):(\d+):(\d+)").Match(result);
                    duration = regex.Groups.Values.TakeLast(3).Zip(times).Select((f, s) => f.First.Value.ToInt() * f.Second).Sum();
                    log.Info($"duration: {duration}");
                    break;
                }
            }

            duration = endTime?.Value?.ToSeconds() ?? duration;
            duration -= startTime?.Value?.ToSeconds() ?? 0;

            return duration;
        }

        private static void ProcessResult(Media media, string result, int duration)
        {
            int current = 0;
            float speed = 1f;
           
            try
            {
                current = new Regex(@"time=(\d+:\d+:\d+)").Match(result).Groups[1].Value.ToSeconds();
                speed = new Regex("speed=([0-9.]*)x").Match(result).Groups[1].Value.ToFloat();
                media.Eta = ((int)((duration - current) / speed)).ToTimeString();
                media.Progress = current * 100 / duration;
            } 
            catch(FormatException)
            {

            }
        }

        private static string NewFile(Media media, List<IParam> parameters)
        {
            VideoCodec video = parameters.Find(e => e is VideoCodec) as VideoCodec;
            AudioCodec audio = parameters.Find(e => e is AudioCodec) as AudioCodec;
            string extension = video?.Extension ?? audio?.Extension ?? Path.GetExtension(media.Path);
            FileInfo newFile = new FileInfo(Path.ChangeExtension(media.Path, extension));
            int inc = 10;

            while(newFile.Exists)
            {
                string name = Path.GetFileNameWithoutExtension(newFile.FullName);
                long counter = DateTimeOffset.Now.ToUnixTimeMilliseconds() % inc;

                newFile = new FileInfo($@"{newFile.DirectoryName}\${name}_{counter}.{extension}");
                inc *= 10;
            }
            log.Info(newFile);

            return newFile.FullName;
        }

        private static int ToSeconds(this string str)
        {
            int[] digits = str.Contains('.') switch
            {
                true => str.Split('.')[0].Split(':').Select(e => e.ToInt()).ToArray(),
                _ => str.Split(':').Select(e => e.ToInt()).ToArray()
            };

            return digits.Count() switch
            {
                2 => digits[0] * 60 + digits[1],
                3 => digits[0] * 3600 + digits[1] * 60 + digits[2],
                _ => digits[0]
            };
        }

        private static string ToTimeString(this int time) =>
            time > 60 ? $"{time / 60}m{time % 60}s" : $"{time % 60}s";

        private static int ToInt(this string str) => int.Parse(str);

        private static float ToFloat(this string str) => float.Parse(str);
    }
}
