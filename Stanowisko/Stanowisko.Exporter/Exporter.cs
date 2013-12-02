using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Stanowisko.SharedClasses;

namespace Stanowisko.Exporter
{
    public static class Exporter
    {

        private static List<IMeasurementExporter> _measurementExporters = new List<IMeasurementExporter>();
        private static SaveFileDialog _saveMeasurementFileDialog = new SaveFileDialog();
        private static List<IExperimentExporter> _experimentExporters = new List<IExperimentExporter>();
        private static SaveFileDialog _saveExperimentFileDialog = new SaveFileDialog();

        static Exporter()
        {
            String filter;

            _measurementExporters.Add(new MockMeasurementExporter("Mock", "*.mck"));
            _measurementExporters.Add(new CSVMeasurementExporter());
            _measurementExporters.Add(new XLSMeasurementExporter());
            filter = "";
            foreach (IMeasurementExporter exporter in _measurementExporters)
            {
                filter += exporter.TypeName + "|" + exporter.TypeExtension + "|";
            }
            _saveMeasurementFileDialog.Filter = filter.TrimEnd(new char[] { '|' });
            _saveMeasurementFileDialog.Title = "Export measurement";
            _saveMeasurementFileDialog.AddExtension = true;

            _experimentExporters.Add(new MockExperimentExporter("Mock", "*.mck"));
            _experimentExporters.Add(new CSVExperimentExporter());
            _experimentExporters.Add(new XLSExperimentExporter());
            filter = "";
            foreach (IMeasurementExporter exporter in _measurementExporters)
            {
                filter += exporter.TypeName + "|" + exporter.TypeExtension + "|";
            }
            _saveExperimentFileDialog.Filter = filter.TrimEnd(new char[] { '|' });
            _saveExperimentFileDialog.Title = "Export experiment";
            _saveExperimentFileDialog.AddExtension = true;
        }

        public static void ExportMeasurement(Measurement measurement)
        {
            if (measurement == null)
                throw new ArgumentNullException();

            _saveMeasurementFileDialog.FileName = measurement.Id.ToString();
            _saveMeasurementFileDialog.ShowDialog();

            if (_saveMeasurementFileDialog.FileName != "")
            {
                _measurementExporters[_saveMeasurementFileDialog.FilterIndex - 1].Export(_saveMeasurementFileDialog, measurement);
            }
        }

        public static void ExportExperiment(Experiment experiment)
        {
            if (experiment == null)
                throw new ArgumentNullException();

            _saveExperimentFileDialog.FileName = experiment.Name;
            _saveExperimentFileDialog.ShowDialog();

            if (_saveExperimentFileDialog.FileName != "")
            {
                _experimentExporters[_saveExperimentFileDialog.FilterIndex - 1].Export(_saveExperimentFileDialog, experiment);
            }
        }
    }
}
