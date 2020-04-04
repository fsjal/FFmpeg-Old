using System.Collections.Generic;

namespace FFmpeg.Model.Codecs
{
    static class Codecs
    {
        public static readonly VideoCodec[] VIDEOS = CreateVideoCodecs();
        public static readonly AudioCodec[] AUDIOS = CreateAudioCodecs();
        public static readonly string[] VIDEO_PRESETS = new string[] { "PRESETS", "SLOW", "MEDIUM", "FAST" };

        private static VideoCodec[] CreateVideoCodecs()
        {
            List<VideoCodec> videos = new List<VideoCodec>();

            videos.Add(new VideoCodec("VIDEO CODEC", null));
            videos.Add(new VideoCodec("H265 GPU", "hevc_nvenc", qualityKey: "-cq"));
            videos.Add(new VideoCodec("H265 CPU", "libx265"));
            videos.Add(new VideoCodec("H264 GPU", "h264_nvenc", qualityKey: "-cq"));
            videos.Add(new VideoCodec("H264 CPU", "libx264"));
            videos.Add(new VideoCodec("WEMB V8", "libvpx", extension: "webm"));
            videos.Add(new VideoCodec("WEMB V9", "libvpx-vp9", extension: "webm"));
            videos.Add(new VideoCodec("GIF", "rgb24", "-pix_fmt", "gif", "-r"));
            videos.Add(new VideoCodec("WMV", "wmv2", extension: "wmv"));
            videos.Add(new VideoCodec("COPY CODEC", "copy"));

            return videos.ToArray();
        }

        private static AudioCodec[] CreateAudioCodecs()
        {
            List<AudioCodec> audios = new List<AudioCodec>();

            audios.Add(new AudioCodec("AUDIO CODEC", null, null));
            audios.Add(new AudioCodec("AAC", "aac", "m4a"));
            audios.Add(new AudioCodec("FLAC", "flac", "flac"));
            audios.Add(new AudioCodec("MP3", "libmp3lame ", "mp3"));
            audios.Add(new AudioCodec("Vorbis", "libvorbis", "ogg"));
            audios.Add(new AudioCodec("COPY CODEC", "copy", null));

            return audios.ToArray();
        }
    }
}
