using System.ComponentModel.Composition;
using System.Text;

namespace QuizMaker.Application.ExportData
{
    [Export(typeof(IExporter))]
    [ExportMetadata(nameof(IExportData.FormatType), "text/csv")]
    [ExportMetadata(nameof(IExportData.FileExtension), ".csv")]
    class CsvExporter : IExporter
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
