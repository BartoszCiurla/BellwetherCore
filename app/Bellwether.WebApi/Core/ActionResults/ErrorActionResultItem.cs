namespace Bellwether.WebApi.Core.ActionResults
{
    public class ErrorActionResultItem
    {
        public string Key { get; }
        public string Value { get; }

        public ErrorActionResultItem(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}
