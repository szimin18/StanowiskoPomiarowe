using System;
using System.Collections.Generic;

namespace Stanowisko.Persistance
{
    public interface ISQLiteDatabase
    {
        List<Dictionary<string, string>> GetAll(String tableName, List<string> columns);
        List<Dictionary<string, string>> GetAll(String tableName, String idName, String idValue, List<String> columns);
        bool Update(String tableName, Dictionary<String, String> data, String where);
        bool Insert(String tableName, Dictionary<String, String> data);
    }
}