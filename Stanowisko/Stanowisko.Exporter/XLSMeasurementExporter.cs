using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Globalization;
using System.IO;
using Stanowisko.SharedClasses;

namespace Stanowisko.Exporter
{
    class XLSMeasurementExporter : IMeasurementExporter
    {
        public XLSMeasurementExporter()
        {
            TypeName = "XLS";
            TypeExtension = "*.xls";
        }

        public bool Export(SaveFileDialog saveFileDialog, Measurement measurement)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application saveApp = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook workbook = saveApp.Workbooks.Open(saveFileDialog.FileName);
                Microsoft.Office.Interop.Excel.Worksheet worksheet = workbook.Worksheets.get_Item(0);
                worksheet.Cells[1, 1] = "Id";
                worksheet.Cells[1, 2] = measurement.Id.ToString(CultureInfo.InvariantCulture);
                worksheet.Cells[2, 1] = "Result";
                worksheet.Cells[2, 2] = measurement.Result.ToString(CultureInfo.InvariantCulture);
                worksheet.Cells[5, 1] = "Id";
                worksheet.Cells[6, 1] = "Time";
                worksheet.Cells[7, 1] = "Value";
                int row = 2;
                foreach (Sample sample in measurement.GetSamples())
                {
                    worksheet.Cells[5, row] = sample.Id.ToString(CultureInfo.InvariantCulture);
                    worksheet.Cells[6, row] = sample.Time.ToString(CultureInfo.InvariantCulture);
                    worksheet.Cells[7, row] = sample.Value.ToString(CultureInfo.InvariantCulture);
                    row++;
                }
                workbook.Close(true, saveFileDialog.FileName);
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
                return false;
            }
            return true;
        }

        public string TypeName { get; private set; }
        public string TypeExtension { get; private set; }
    }
}
