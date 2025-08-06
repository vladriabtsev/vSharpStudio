namespace vSharpStudio.common
{
    public class Result<TValue>
    {
        public Result() { this.IsContinue = true; }
        public bool IsContinue { get; set; }
        public TValue? Value { get; set; }
    }
}
