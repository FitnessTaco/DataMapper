using System;
using System.Data;
using System.Data.Common;

namespace dottech.data;

public static class DataExtensions
{
    // test comment
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
    public static string GetString<T>(this T row, string name) where T : DataRow
    {
        return row.GetValue(name)?.ToString() ?? string.Empty;
    }
    // public static string GetString(this DataRow row, string name)
    // {
    //     return row.GetValue(name)?.ToString() ?? string.Empty;
    // }
    public static int? GetInt(this DataRow row, string name)
    {
        var value = row.GetValue(name);
        if (value == null) return null;
        return  Convert.ToInt32(value);
    }
    public static int? GetIntOrDefault(this DataRow row, string name, int defaultValue = default(int))
    {
        var value = row.GetValue(name);
        if (value == null) return defaultValue;
        return Convert.ToInt32(value);
    }
    public static double? GetDouble(this DataRow row, string name)
    {
        var value = row.GetValue(name);
        if (value == null) return null;
        return Convert.ToDouble(value);
    }  
    public static decimal? GetDecimal(this DataRow row, string name)
    {
        var value = row.GetValue(name);
        if (value == null) return null;
        return Convert.ToDecimal(value);
    }     
    public static DateTime? GetDate(this DataRow row, string name)
    {
        var value = row.GetValue(name);
        if (value == null) return null;
        return Convert.ToDateTime(value);
    } 
    public static bool? GetBool(this DataRow row, string name)
    {
        var value = row.GetValue(name);
        if (value == null) return null;
        return Convert.ToBoolean(value);
    } 
    private static object? GetValue(this DataRow row, string name)
    {
        return row != null && row.Table.Columns.Contains(name) && row[name] != DBNull.Value && row[name] != null
            ? row[name]
            : null;
    }
}
