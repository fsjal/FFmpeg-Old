namespace FFmpeg.Model.Codecs
{
    class SimpleParam : IParam
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public SimpleParam(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public SimpleParam(string key, int? value)
        {
            Key = key;
            if (value != null) Value = value.ToString();
        }

        public override string ToString() => $"{Key} {Value}";
    }
}
