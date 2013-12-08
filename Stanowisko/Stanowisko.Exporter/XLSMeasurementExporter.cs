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
                Microsoft.Office.Interop.Excel.Workbook workbook = saveApp.Workbooks.Add(System.Reflection.Missing.Value);
                Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];
                worksheet.Cells[1, 1] = "Id";
                worksheet.Cells[1, 2] = measurement.Id.ToString(CultureInfo.InvariantCulture);
                worksheet.Cells[2, 1] = "Result";
                worksheet.Cells[2, 2] = measurement.Result.ToString(CultureInfo.InvariantCulture);
                worksheet.Cells[1, 5] = "Id";
                worksheet.Cells[1, 6] = "Time";
                worksheet.Cells[1, 7] = "Value";
                int row = 2;
                foreach (Sample sample in measurement.GetSamples())
                {
                    worksheet.Cells[row, 5] = sample.Id.ToString(CultureInfo.InvariantCulture);
                    worksheet.Cells[row, 6] = sample.Time.ToString(CultureInfo.InvariantCulture);
                    worksheet.Cells[row, 7] = sample.Value.ToString(CultureInfo.InvariantCulture);
                    row++;
                }
                workbook.SaveAs(saveFileDialog.FileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal);
                workbook.Close(true);
                saveApp.Quit();
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
