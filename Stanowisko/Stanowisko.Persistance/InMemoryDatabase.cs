using System;
using System.Collections.Generic;
using System.Linq;

namespace Stanowisko.Persistance
{
    public class InMemoryDatabase : IDatabase
    {
        public List<Dictionary<string, string>> Experiments = new List<Dictionary<string, string>>();

        public List<Dictionary<string, string>> Measurements = new List<Dictionary<string, string>>();

        public List<Dictionary<string, string>> Samples = new List<Dictionary<string, string>>();

        public List<Dictionary<string, string>> Parameters = new List<Dictionary<string, string>>();

        private List<Dictionary<string, string>> _table;

        private void SetTable(string tableName)
        {
            switch (tableName)
            {
                case "Samples":
                    _table = Samples;
                    break;
                case "Measurements":
                    _table = Measurements;
                    break;
                case "Experiments":
                    _table = Experiments;
                    break;
                case "Parameters":
                    _table = Parameters;
                    break;
            }
        }

        public List<Dictionary<string, string>> GetAll(string tableName, List<string> columns)
        {
            SetTable(tableName);

            return (from row in _table
                    select columns.ToDictionary(column => column, column => row[column])).ToList();
        }

        public List<Dictionary<string, string>> GetAll(string tableName, string idName, string idValue, List<string> columns)
        {
            SetTable(tableName);

            return (from row in _table
                    where row[idName] == idValue
                    select columns.ToDictionary(column => column, column => row[column])).ToList();
        }

        public List<Dictionary<string, string>> GetAll(string pTableName, string pIdName, string pIdValue, string sIdName, string sIdValue, List<string> columns)
        {
            SetTable(pTableName);

            return (from row in _table
                    where row[pIdName] == pIdValue
                    where row[sIdName] == sIdValue
                    select columns.ToDictionary(column => column, column => row[column])).ToList();
        }

        public bool Update(string tableName, Dictionary<string, string> data, string where)
        {
            SetTable(tableName);

            var e = _table.Find(dictionary => dictionary["ID"] == data["ID"]);
            var i = _table.IndexOf(e);

            _table[i] = data;

            return false;
        }

        public bool Insert(string tableName, Dictionary<string, string> data)
        {
            SetTable(tableName);

            _table.Add(data);

            return true;
        }

        public int GetNextExperimentID()
        {
            return Experiments.Count > 0 ? Experiments.Select(e => Convert.ToInt32(e["ID"])).Max() + 1 : 1;
        }

        public int GetNextMeasurementID(int eId)
        {
            return Measurements.Count > 0
                       ? Measurements.Where(m => m["Id"] == eId.ToString())
                             .Select(m => Convert.ToInt32(m["ID"])).Max() + 1
                       : 1;
        }

        public int GetNextSampleID(int eId, int mId)
        {
            return Samples.Count > 0
                       ? Samples.Where(s => s["measurement"] == mId.ToString() && s["experiment"] == eId.ToString())
                             .Select(s => Convert.ToInt32(s["ID"])).Max() + 1
                       : 1;
        }
    }
}
