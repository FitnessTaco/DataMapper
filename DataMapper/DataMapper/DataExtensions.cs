using System;
using System.ComponentModel;
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
        return Convert.ToInt32(value);
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

    public static struct GetValue(this DataRow row, string name)
    {
        
    }
    public static T? GetValue<T>(this DataRow row, string name, T? prototype = null) where T : struct
    {
        if (row == null || !row.Table.Columns.Contains(name)) return null;
        var value = row[name];
        if (value == null || value == DBNull.Value) return null;
        // check for conversions between datetime and dateonly
        if (typeof(T) == typeof(DateOnly) && value is DateTime dateTime)
        {
            return (T)(object)DateOnly.FromDateTime(dateTime);
        }
        // check for conversions between dateonly and datetime
        if (typeof(T) == typeof(DateTime) && value is DateOnly dateOnly)
        {
            return (T)(object)dateOnly.ToDateTime(TimeOnly.MinValue);
        }
        // check for conversions to Enum from int or string
        if (typeof(T).IsEnum && value is int intValue)
        {
            return (T)Enum.ToObject(typeof(T), intValue);
        }
        // check for conversions to Enum values
        if (typeof(T).IsEnum && value is string strValue)
        {
            return (T)Enum.Parse(typeof(T), strValue, true);
        }

        return (T)System.Convert.ChangeType(value, typeof(T));

    }
}
