using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stanowisko.Exporter
{
    public interface IExporterFactory<T>
    {
        string FileType { get; }
        string FileExtension { get; }

        Exporter<T> GetExporter();
    }
}
