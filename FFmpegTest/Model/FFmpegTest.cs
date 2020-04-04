using FFmpeg.Entities;
using FFmpeg.Model;
using FFmpeg.Model.Codecs;
using FFmpeg.Util;
using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace FFmpegTest.Model
{
    public class FFmpegTest
    {

        [Fact]
        public async void TestDuration()
        {
            var sourceToken = new CancellationTokenSource();
            var media = new Media { Path = @"D:\Downloads\Video\Vicetone & Tony Igy - Astronomia - YouTube.mkv" };
            int duration = await FFmpegConverter.GetDurationAsync(media, new List<IParam>(), sourceToken.Token);
            Logger.Log.Info(duration);
            Assert.NotEqual(0, duration);
        }
    }
}
