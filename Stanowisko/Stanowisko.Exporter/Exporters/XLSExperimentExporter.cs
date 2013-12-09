using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.IO;
using Microsoft.Office.Core;
using Microsoft.Office.Interop;

using Stanowisko.SharedClasses;

namespace Stanowisko.Exporter.Exporters
{
    internal class XLSExperimentExporterFactory : ExporterFactory<Experiment>
    {
        private static readonly string _fileType = "Excel";
        private static readonly string _fileExtension = "*.xls";

        public XLSExperimentExporterFactory()
            : base(_fileType, _fileExtension, typeof(XLSExperimentExporter))
        {
        }

        internal class XLSExperimentExporter : Exporter<Experiment>
        {
            public XLSExperimentExporter()
                : base(_fileType, _fileExtension)
            {
            }

            public override void ExportToFile(Experiment experiment)
            {
                try
                {
                    Microsoft.Office.Interop.Excel.Application saveApp = new Microsoft.Office.Interop.Excel.Application();
                    Microsoft.Office.Interop.Excel.Workbook workbook = saveApp.Workbooks.Add(System.Reflection.Missing.Value);
                    Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];
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
                    workbook.SaveAs(OutputFileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal);
                    workbook.Close(true);
                    saveApp.Quit();
                }
                catch (Exception e)
                {
                    Console.Write(e.Message);
                    Console.Write(e.StackTrace);
                }
            }
        }
    }
}
