﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Globalization;
using System.IO;
using Stanowisko.SharedClasses;

namespace Stanowisko.Exporter
{
    class CSVMeasurementExporter : IMeasurementExporter
    {
        public CSVMeasurementExporter()
        {
            TypeName = "CSV";
            TypeExtension = "*.csv";
        }

        public bool Export(FileStream fileStream, Measurement measurement)
        {
            try
            {
                StreamWriter streamWriter = new StreamWriter(fileStream);
                streamWriter.WriteLine("Result," + measurement.Result.ToString(CultureInfo.InvariantCulture));
                streamWriter.WriteLine();
                streamWriter.WriteLine("Time,Value");
                foreach (Sample sample in measurement.GetSamples() )
                {
                    streamWriter.WriteLine(sample.Time.ToString(CultureInfo.InvariantCulture) +
                        "," + sample.Value.ToString(CultureInfo.InvariantCulture));
                }
                streamWriter.Close();
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