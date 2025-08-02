using QuizMaker.Application.DTOs;
using QuizMaker.Application.ExportData;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace QuizMaker.Application.Services
{
    public class QuizExportService : IQuizExportService
    {
        [ImportMany]
        private IEnumerable<Lazy<IExporter, IExportData>> exporters;

        private CompositionContainer _container;
        public QuizExportService()
        {
            Initialize();
        }

        private void Initialize()
        {
            // An aggregate catalog that combines multiple catalogs.
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(IExporter).Assembly));

            // Path for development
            var extensionsPath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "ExtensionsLibrary");

            if (Directory.Exists(extensionsPath))
            {
                catalog.Catalogs.Add(new DirectoryCatalog(extensionsPath));
            }

            _container = new CompositionContainer(catalog);
            _container.ComposeParts(this);

        }

        public IEnumerable<string> GetAvailableExportTypes()
        {
            var exportTypes = new List<string>();
            foreach (Lazy<IExporter, IExportData> i in exporters)
            {
                exportTypes.Add(i.Metadata.FormatType.ToString());
            }
            return exportTypes;
        }

        public async Task<ExportDataDTO?> GetExportDataByType(QuizDTO quiz, string exportType)
        {
            var exporter = exporters.FirstOrDefault(e => e.Metadata.FormatType.Equals(exportType));
            if (exporter == null)
            {
                return null;
            }
            var byteData = exporter.Value.Export(quiz.Questions.Select(x => x.QuestionText));
            return new ExportDataDTO(byteData, $"{quiz.Title.Replace(" ", "_")}{exporter.Metadata.FileExtension}");
        }
    }
}
