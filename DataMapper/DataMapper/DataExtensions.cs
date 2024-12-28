using System;
using System.Data;

namespace dottech.data;

public static class DataExtensions
{
 /*   public static string GetString(this DataRow row, string name)
    {
        if (row == null) return string.Empty;
        if (!row.Table.Columns.Contains(name)) return string.Empty;
        if (row[name] == DBNull.Value || row[name] == null) return string.Empty;
        return row[name].ToString();
        
    }
        public static T? Get<T>(this DataRow row, string name)
    {
        return row != null && row.Table.Columns.Contains(name) && row[name] != DBNull.Value && row[name] != null
            ?  row[name]
                        : default(T);
    }
*/
    public static string GetString(this DataRow row, string name)
    {
        return row.GetValue(name)?.ToString() ?? string.Empty;
    }
    public static int? GetInt(this DataRow row, string name)
    {
        return Convert.ToInt32(row.GetValue(name));
    }
    public static double? GetDouble(this DataRow row, string name)
    {
        return Convert.ToDouble(row.GetValue(name));
    }  
    public static decimal? GetDecimal(this DataRow row, string name)
    {
        return Convert.ToDecimal(row.GetValue(name));
    }     
    public static DateTime? GetDate(this DataRow row, string name)
    {
        return Convert.ToDateTime(row.GetValue(name));
    } 
    public static bool? GetBool(this DataRow row, string name)
    {
        return Convert.ToBoolean(row.GetValue(name));
    } 
    private static object? GetValue(this DataRow row, string name)
    {
        return row != null && row.Table.Columns.Contains(name) && row[name] != DBNull.Value && row[name] != null
            ? row[name]
            : null;
    }
}
