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
    }
}
