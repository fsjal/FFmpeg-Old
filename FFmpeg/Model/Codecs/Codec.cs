namespace FFmpeg.Model.Codecs
{
    public class Codec : IParam
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }

        public Codec(string name, string key, string value, string extension)
        {
            Name = name;
            Key = key;
            Value = value;
            Extension = extension;
        }

        public override string ToString() => Name;
    }
}
