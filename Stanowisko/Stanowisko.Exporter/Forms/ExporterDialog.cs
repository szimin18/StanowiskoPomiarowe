using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stanowisko.Exporter.Forms
{
    public abstract class ExporterDialog<T>
    {
        #region Private fields and properties

        private IList<IExporterFactory<T>> _exporterFilter;
        private string _exporterFilterString;

        private bool IsExporterFilterUsed
        {
            get
            {
                return (_exporterFilterString == Dialog.Filter) || _exporterFilterString.Equals(Dialog.Filter);
            }
        }

        #endregion

        #region Private methods

        private string GetFilterStringFromFactory(IExporterFactory<T> factory)
        {
            return factory.FileType + "|" + factory.FileExtension;
        }

        private string GetFilterStringFromFactories(ICollection<IExporterFactory<T>> factories)
        {
            string filter = "";
            foreach (IExporterFactory<T> factory in factories)
            {
                filter += GetFilterStringFromFactory(factory);
            }
            filter.TrimEnd(new char[] {'|'});

            return filter;
        }
        
        #endregion

        #region Public fields and properties

        public readonly SaveFileDialog Dialog;

        public ICollection<IExporterFactory<T>> ExporterFilter
        {
            get
            {
                return _exporterFilter;
            }
            set
            {
                _exporterFilter = new List<IExporterFactory<T>>(value);
                _exporterFilterString = GetFilterStringFromFactories(_exporterFilter);
                Dialog.Filter = _exporterFilterString;
            }
        }

        #endregion

        #region Public methods

        public abstract T Exportee
        {
            get;
            set;
        }

        public Exporter<T> GetExporter()
        {
            if (IsExporterFilterUsed)
            {
                Exporter<T> exporter = _exporterFilter[Dialog.FilterIndex].GetExporter();
                exporter.OutputFileName = Dialog.FileName;
                return exporter;
            }
            else
                return null;
        }

        #endregion

        #region Constructors

        public ExporterDialog()
        {
            Dialog = new SaveFileDialog();
        }

        #endregion
    }
}
