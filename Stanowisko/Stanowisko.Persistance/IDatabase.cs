using System;
using System.Collections.Generic;
using Stanowisko.SharedClasses;

namespace Stanowisko.Persistance
{
    public interface IDatabase
    {
        List<Dictionary<string, string>> GetAll(String tableName, List<string> columns);

        List<Dictionary<string, string>> GetAll(String tableName, String idName, String idValue, List<String> columns);

        List<Dictionary<string, string>> GetParameters(String pTableName, String pIdName, String pIdValue,
            String sIdName, String sIdValue, List<String> columns);

        bool UpdateParameters(Dictionary<string, string> data, string where);

        bool Update(String tableName, Dictionary<String, String> data, String where);

        bool Insert(String tableName, Dictionary<String, String> data);

        int GetNextExperimentID();

        int GetNextMeasurementID(String eId);

        int GetNextSampleID(String eId, String mId);
    }
}