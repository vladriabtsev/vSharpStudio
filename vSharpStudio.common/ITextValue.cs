namespace vSharpStudio.common
{
    public interface ITextValue
    {
        string Text { get; }
        string Value { get; }
    }
    public class TextValue : ITextValue
    {
        public string Text { get; set; } = "";
        public string Value { get; set; } = "";
    }
}
