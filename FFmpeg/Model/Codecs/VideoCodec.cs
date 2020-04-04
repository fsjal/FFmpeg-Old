namespace FFmpeg.Model.Codecs
{
    public class VideoCodec : Codec
    {
        public string QualityKey { get; set; }

        public VideoCodec(string name, string libName, string key = "-c:v", string extension = "mp4", string qualityKey = "-crf") :
            base(name, key, libName, extension)
        {
            QualityKey = qualityKey;
        }
    }
}
