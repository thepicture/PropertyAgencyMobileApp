namespace PropertyAgencyMobileApp.Models
{
    public class OptionEventType
    {
        public OptionEventType(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; }

        public string Value { get; }
    }
}
