using System.ComponentModel.Composition;
using System.Text;
using System.Text.Json;

namespace QuizMaker.Application.ExportData
{
    [Export(typeof(IExporter))]
    [ExportMetadata(nameof(IExportData.FormatType), "application/json")]
    [ExportMetadata(nameof(IExportData.FileExtension), ".json")]
    class JsonExporter : IExporter
    {
        public byte[] Export(IEnumerable<string> questions)
        {
            string jsonString = JsonSerializer.Serialize(questions, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            return Encoding.UTF8.GetBytes(jsonString);
        }
    }
}
