using System;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using Microsoft.VisualBasic;

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

    // public static T GetEnum<T>(this DataRow row, string name) where T : Enum
    // {
    //     var value = row.GetValue(name);
    //     CheckThrowIfNull(value, typeof(T), name);
    //     if (value is int intValue)
    //     {
    //         return (T)value;  
    //     }
    //     if (value is string strValue)
    //     {
    //         return (T)Enum.Parse(typeof(T), strValue, true);
    //     }
    //     throw new InvalidCastException($"Cannot convert {value!.GetType()} to {typeof(T)}");
    // }
    public static Enum? GetNullableEnum(this DataRow row, string name) 
    {
        var value = row.GetValue(name);
        if (value == null || value == DBNull.Value) return default(Enum);
        if (value is int intValue)
        {
            
            return (Enum)value;
           // var v2 = (System.Enum)System.Enum.Parse(typeof(T), Convert.ToInt32(value).ToString());
            // var v = Enum.ToObject(typeof(T), value); 
            // return (T)Enum.ToObject(typeof(T), value);  
        }
        if (value is string strValue)
        {
            //return (T)Enum.Parse(typeof(T), strValue, true);
        }
        throw new InvalidCastException($"Cannot convert {value.GetType()} to {typeof(Enum)}");
    }

        public static T? GetEnum<T>(this DataRow row, string fieldName) where T : struct, Enum
		{
            return (T?)GetEnum(row, fieldName, typeof(T));
		}

        
        public static Enum? GetEnum(this DataRow row, string fieldName, Type enumType) 
		{
            var v = row.GetString(fieldName);
            if (v == null || v == string.Empty) return null;
            return System.Enum.Parse(enumType, v) as Enum;            
		}

    public static T? GetValueOf<T>(this DataRow row, string name, T? prototype = null) where T : struct
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

    private static void CheckThrowIfNull(object? value, Type type, string columnName)
    {
        if (value == null || value == DBNull.Value)
        {
            throw new ArgumentNullException(nameof(value), $"The value for type column {columnName} cannot be null or DBNull.");
        }
    }
    
}
