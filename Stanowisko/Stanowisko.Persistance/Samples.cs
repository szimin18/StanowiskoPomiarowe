using System.Collections.Generic;
using Stanowisko.SharedClasses;

namespace Stanowisko.Persistance
{
    class Samples
    {
        readonly SQLiteDatabase _db = new SQLiteDatabase();

        public void Add(Sample sample, Measurement measurement)
        {
            var data = new Dictionary<string, string>();
            data.Add("ID", sample.Id.ToString());
            data.Add("measurement", measurement.Id.ToString());
            data.Add("value", sample.Value.ToString());
            data.Add("time", sample.Time.ToString());
            try
            {
                _db.Insert("Samples", data);
            }
            catch
            {

            }
        }
    }
}
