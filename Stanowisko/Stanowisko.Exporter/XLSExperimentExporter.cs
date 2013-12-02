using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Core;
using Microsoft.Office.Interop;

using System.Globalization;
using System.IO;
using Stanowisko.SharedClasses;

namespace Stanowisko.Exporter
{
    class XLSExperimentExporter : IExperimentExporter
    {
        public XLSExperimentExporter()
        {
            TypeName = "XLS";
            TypeExtension = "*.xls";
        }

        public bool Export(SaveFileDialog saveFileDialog, Experiment experiment)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application saveApp = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook workbook = saveApp.Workbooks.Open(saveFileDialog.FileName);
                Microsoft.Office.Interop.Excel.Worksheet worksheet = workbook.Worksheets.get_Item(0);
                worksheet.Cells[1, 1] = "Id";
                worksheet.Cells[1, 2] = experiment.Id.ToString(CultureInfo.InvariantCulture);
                worksheet.Cells[2, 1] = "Name";
                worksheet.Cells[2, 2] = experiment.Name;
                worksheet.Cells[3, 1] = "Description";
                worksheet.Cells[3, 2] = experiment.Description;
                worksheet.Cells[4, 1] = "Goal";
                worksheet.Cells[4, 2] = experiment.Goal;
                worksheet.Cells[5, 1] = "Result";
                worksheet.Cells[5, 2] = experiment.Result.ToString(CultureInfo.InvariantCulture);
                worksheet.Cells[6, 1] = "Summary";
                worksheet.Cells[6, 2] = experiment.Summary;
                worksheet.Cells[1, 4] = "Key";
                worksheet.Cells[1, 5] = "Value";
                int row = 2;
                foreach (string key in experiment.GetParameters().Keys)
                {
                    worksheet.Cells[row, 4] = key;
                    worksheet.Cells[row, 5] = experiment.GetParameters()[key];
                    row++;
                }
                worksheet.Cells[1, 7] = "Id";
                worksheet.Cells[1, 8] = "Result";
                row = 2;
                foreach (Measurement measurement in experiment.GetMeasurements())
                {
                    worksheet.Cells[row, 7] = measurement.Id.ToString(CultureInfo.InvariantCulture);
                    worksheet.Cells[row, 8] = measurement.Result.ToString(CultureInfo.InvariantCulture);
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
