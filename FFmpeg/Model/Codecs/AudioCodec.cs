namespace FFmpeg.Model.Codecs
{
    public class AudioCodec : Codec
    {
        public AudioCodec(string name, string libName, string extension) : base(name, "-c:a", libName, extension)
        {

        }

    }
}
