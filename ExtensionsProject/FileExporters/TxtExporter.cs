using QuizMaker.Application.ExportData;
using System.ComponentModel.Composition;
using System.Text;

namespace FileExporters
{
    [Export(typeof(IExporter))]
    [ExportMetadata(nameof(IExportData.FormatType), "text/plain")]
    [ExportMetadata(nameof(IExportData.FileExtension), ".txt")]
    class TxtExporter : IExporter
    {
        public byte[] Export(IEnumerable<string> questions)
        {
            var builder = new StringBuilder();
            foreach (var question in questions)
            {
                builder.AppendLine(question);
            }
            return Encoding.UTF8.GetBytes(builder.ToString());
        }
    }
}
