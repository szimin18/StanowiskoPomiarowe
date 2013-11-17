using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stanowisko.Persistance;

namespace Stanowisko.Testy
{
    class DataBaseMock : ISQLiteDatabase
    {
        public List<Dictionary<string, string>> Experiments = new List<Dictionary<string, string>>();
        public List<Dictionary<string, string>> Measurements = new List<Dictionary<string, string>>();
        public List<Dictionary<string, string>> Samples = new List<Dictionary<string, string>>();

        private List<Dictionary<string, string>> table;

        private void setTable(string tableName)
        {
            switch (tableName)
            {
                case "Samples":
                    table = Samples;
                    break;
                case "Measurements":
                    table = Measurements;
                    break;
                case "Experiments":
                    table = Experiments;
                    break;
}
        }

        public List<Dictionary<string, string>> GetAll(string tableName, List<string> columns)
        {
            return new List<Dictionary<string, string>>();
        }

        public List<Dictionary<string, string>> GetAll(string tableName, string idName, string idValue, List<string> columns)
        {
            setTable(tableName);

            return (from row in table
                    where row[idName] == idValue
                    select columns.ToDictionary(column => column, column => row[column])).ToList();
        }

        public bool Update(string tableName, Dictionary<string, string> data, string where)
        {
            setTable(tableName);

            var e = table.Find(dictionary => dictionary["ID"] == data["ID"]);
            var i = table.IndexOf(e);

            table[i] = data;

            return false;
        }

        public bool Insert(string tableName, Dictionary<string, string> data)
        {
            setTable(tableName);
            table.Add(data);
            return true;
        }
    }
}
