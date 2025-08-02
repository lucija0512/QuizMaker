namespace QuizMaker.Application.ExportData
{
    public interface IExporter
    {
        byte[] Export(IEnumerable<string> questions);
    }

    public interface IExportData
    {
        string FormatType { get; }
        string FileExtension { get;  }
    }
}