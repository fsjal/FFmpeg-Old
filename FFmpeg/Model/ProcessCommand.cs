using FFmpeg.Util;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Channels;

namespace FFmpeg.Model
{
    public class ProcessCommand
    {

        private readonly Process process = new Process();
        private readonly Channel<string> channel = Channel.CreateBounded<string>(1);

        public ProcessCommand(string arguments, CancellationToken token)
        {   
            process.StartInfo.FileName = @"C:\Program Files\FFmpeg\bin\ffmpeg.exe";
            process.StartInfo.Arguments = arguments;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.OutputDataReceived += (s, e) => channel.Writer.WriteAsync(e.Data?.ToString(), token);
            process.ErrorDataReceived += (s, e) => channel.Writer.WriteAsync(e.Data?.ToString(), token);
        }

        public async IAsyncEnumerable<string> ResultAsync([EnumeratorCancellation] CancellationToken token)
        {
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            string result = await channel.Reader.ReadAsync(token);
            while (result != null && !token.IsCancellationRequested)
            {
                token.ThrowIfCancellationRequested();
                yield return result;
                result = await channel.Reader.ReadAsync(token);
            }
            process.WaitForExit();
            process.Close();
        }
    }
}
