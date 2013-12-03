using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Stanowisko.SharedClasses;
using Stanowisko.Exporter.Exporters;
using Stanowisko.Exporter.Forms;

namespace Stanowisko.Exporter
{
    public class ExporterTests
    {
        public void Test()
        {
            TestExporterFactory();
            TestExporterDialog();
        }

        public void TestExporterFactory()
        {
            ICollection<IExporterFactory<Measurement>> factories = ExporterFactory<Measurement>.GetAllExporters();
            Assert.IsNotNull(factories);
            Assert.IsTrue(factories.Count > 0);
            foreach (ExporterFactory<Measurement> factory in factories)
            {
                Exporter<Measurement> exporter = factory.GetExporter();
                Assert.IsNotNull(exporter);
                Assert.AreEqual(factory.FileType, exporter.FileType);
                Assert.AreEqual(factory.FileExtension, exporter.FileExtension);

                Assert.AreEqual(factory.FileType, 
                    ExporterFactory<Measurement>.GetExporterByType(factory.FileType).FileType);
                Assert.AreEqual(factory.FileExtension, 
                    ExporterFactory<Measurement>.GetExporterByExtension(factory.FileExtension).FileExtension);
            }
        }

        public void TestExporterDialog()
        {
            MeasurementExporterDialog dialog = new MeasurementExporterDialog();
            dialog.ExporterFilter = ExporterFactory<Measurement>.GetAllExporters();
            dialog.Dialog.ShowDialog();
        }
    }
}
