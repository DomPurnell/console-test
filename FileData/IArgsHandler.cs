namespace FileData
{
    public interface IArgsHandler
    {
        string Filename { get; }
        bool ArgsValid { get; }
        SearchTypeEnum SearchType { get; }
    }
}
