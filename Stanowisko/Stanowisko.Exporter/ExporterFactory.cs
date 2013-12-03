using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stanowisko.Exporter
{
    public abstract class ExporterFactory<T> : IExporterFactory<T>
    {
        private static IEnumerable<ExporterFactory<T>> _allExporters;

        public static ICollection<IExporterFactory<T>> GetAllExporters()
        {
            return new List<IExporterFactory<T>>(_allExporters);
        }

        public static ExporterFactory<T> GetExporterByType(string type)
        {
            return _allExporters.First((ExporterFactory<T> exporter) => exporter.FileType.Equals(type));
        }

        public static ExporterFactory<T> GetExporterByExtension(string type)
        {
            return _allExporters.First((ExporterFactory<T> exporter) => exporter.FileExtension.Equals(type));
        }

        static ExporterFactory()
        {
            _allExporters = typeof(ExporterFactory<T>).Assembly
                .GetTypes()
                .Where(t => t.IsSubclassOf(typeof(ExporterFactory<T>)) && 
                        !t.IsAbstract && 
                        t.GetConstructor(new Type[]{}) != null)
                .Select(t => (ExporterFactory<T>) Activator.CreateInstance(t));
        }

        private readonly string _fileType;
        private readonly string _fileExtension;
        private readonly Type _exporterType;

        public string FileType
        {
            get { return _fileType; }
        }
        public string FileExtension
        {
            get { return _fileExtension; }
        }

        public Exporter<T> GetExporter()
        {
            return (Exporter<T>) _exporterType.GetConstructor(new Type[] {}).Invoke(new object[] {});
        }

        internal ExporterFactory(string fileType, string fileExtension, Type exporterType)
        {
            _fileType = fileType;
            _fileExtension = fileExtension;
            _exporterType = exporterType;
        }
    }
}
