using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SQLite;

namespace Stanowisko.Persistance
{

    class SQLiteDatabase
    {
        readonly String _dbConnection;

        public SQLiteDatabase()
        {
            _dbConnection = "Data Source=CHANGME";
        }

        public SQLiteDatabase(string inputFile)
        {
            _dbConnection = String.Format("Data Source={0}", inputFile);
        }

        public DataTable GetDataTable(string sql)
        {
            var dt = new DataTable();
            try
            {
                var cnn = new SQLiteConnection(_dbConnection);
                cnn.Open();
                var mycommand = new SQLiteCommand(cnn);
                mycommand.CommandText = sql;
                var reader = mycommand.ExecuteReader();
                dt.Load(reader);
                reader.Close();
                cnn.Close();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return dt;
        }

        public int ExecuteNonQuery(string sql)
        {
            var cnn = new SQLiteConnection(_dbConnection);
            cnn.Open();
            var mycommand = new SQLiteCommand(cnn) {CommandText = sql};
            var rowsUpdated = mycommand.ExecuteNonQuery();
            cnn.Close();
            return rowsUpdated;
        }


        public string ExecuteScalar(string sql)
        {
            var cnn = new SQLiteConnection(_dbConnection);
            cnn.Open();
            var mycommand = new SQLiteCommand(cnn) {CommandText = sql};
            var value = mycommand.ExecuteScalar();
            cnn.Close();
            return value != null ? value.ToString() : "";
        }

        public bool Update(String tableName, Dictionary<String, String> data, String where)
        {
            var vals = "";
            var returnCode = true;
            if (data.Count >= 1)
            {
                vals = data.Aggregate(vals, (current, val) => 
                    current + String.Format(" {0} = '{1}',", val.Key.ToString(), val.Value.ToString()));
                vals = vals.Substring(0, vals.Length - 1);
            }
            try
            {
                ExecuteNonQuery(String.Format("update {0} set {1} where {2};", tableName, vals, where));
            }
            catch (Exception)
            {
                returnCode = false;
            }
            return returnCode;
        }
        public bool Delete(String tableName, String where)
        {
            var returnCode = true;
            try
            {
                ExecuteNonQuery(String.Format("delete from {0} where {1};", tableName, where));
            }
            catch (Exception)
            {
                returnCode = false;
            }
            return returnCode;
        }
        public bool Insert(String tableName, Dictionary<String, String> data)
        {
            String columns = "";
            String values = "";
            var returnCode = true;
            foreach (KeyValuePair<String, String> val in data)
            {
                columns += String.Format(" {0},", val.Key);
                values += String.Format(" '{0}',", val.Value);
            }
            columns = columns.Substring(0, columns.Length - 1);
            values = values.Substring(0, values.Length - 1);
            try
            {
                ExecuteNonQuery(String.Format("insert into {0}({1}) values({2});", tableName, columns, values));
            }
            catch (Exception)
            {
                returnCode = false;
            }
            return returnCode;
        }

    }
}
