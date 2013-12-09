using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Stanowisko.Exporter
{
    public abstract class Exporter<T>
    {
        public readonly string FileType;
        public readonly string FileExtension;

        public string OutputFileName
        {
            get;
            set;
        }

        internal Exporter(string fileType, string fileExtension)
        {
            if (fileType == null)
                throw new ArgumentNullException("fileType");
            if (fileExtension == null)
                throw new ArgumentNullException("fileExtension");
            if (fileType.Contains('|'))
                throw new ArgumentException("fileType cannot contain \'|\' character");
            if (fileExtension.Contains('|'))
                throw new ArgumentException("fileExtension cannot contain \'|\' character");
            /* check whether "<fileType>|<fileExtension>" is a proper SaveFileDialog.Filter */
            try
            {
                new SaveFileDialog().Filter = fileType + "|" + fileExtension;
            }
            catch (Exception filterException)
            {
                throw new ArgumentException("fileType or fileExtension invalid", filterException);
            }

            FileType = fileType;
            FileExtension = fileExtension;
        }

        protected void Export(T exportee)
        {
            ExportToFile(exportee);
            DialogResult dialogResult = MessageBox.Show("Czy chcesz otworzyć plik w domyślnym programie?", "Otwieranie pliku", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start(OutputFileName);
            }
        }

        public abstract void ExportToFile(T exportee);
    }
}
