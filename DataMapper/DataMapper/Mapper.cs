

using System.Configuration.Assemblies;
using System.Data;
using System.Reflection;
using DataMapper;

namespace dottech.data;

public class Mapper
{
    // make a static method to initialize an object decorated with MapToTable and MapTo attributes
    public static T Map<T>(DataRow row) where T : new()
    {
        if (row == null) throw new ArgumentNullException(nameof(row));

        var obj = new T();
        var type = typeof(T);

        // Get the table name from the MapToTable attribute
        var tableAttribute = type.GetCustomAttributes(typeof(MapToTable), false);
        if (tableAttribute.Length == 0)
            throw new InvalidOperationException($"Class {type.Name} is not decorated with MapToTable attribute.");

        foreach (var prop in type.GetProperties())
        {
            // Skip properties with DataIgnoreMap attribute
            if (Attribute.IsDefined(prop, typeof(DataIgnoreMap))) continue;

            // Get the column name from the MapTo attribute
            var mapToAttribute = prop.GetCustomAttributes(typeof(MapTo), false);
            if (mapToAttribute.Length > 0)
            {
                var columnName = ((MapTo)mapToAttribute[0]).ColumnName;
                if (row.Table.Columns.Contains(columnName))
                {
                    // call row.GetValue<T>(columnName) to get the value
                    // and set the value to the property
                    // if the property is a nullable type, we need to check for DBNull
                    switch (prop.PropertyType)
                    {
                        case Type t when t == typeof(string):
                            // For string properties, we can directly set the value
                            var stringValue = row.GetString(columnName);
                            CheckThrowIfNull(stringValue, prop.PropertyType, columnName);
                            prop.SetValue(obj, stringValue);
                            break;

                        case Type t when t == typeof(int):
                            // For int properties, we can directly set the value
                            var intValue = row.GetInt(columnName);
                            CheckThrowIfNull(intValue, prop.PropertyType, columnName);
                            prop.SetValue(obj, intValue);
                            break;
                        case Type t when t == typeof(int?):
                            // For int properties, we can directly set the value
                            var intValue2 = row.GetInt(columnName);
                            if (intValue2.HasValue)
                                prop.SetValue(obj, intValue2);
                            break;

                        case Type t when t == typeof(double):
                            // For double properties, we can directly set the value
                            var dblValue = row.GetDouble(columnName);
                            CheckThrowIfNull(dblValue, prop.PropertyType, columnName);
                            prop.SetValue(obj, dblValue);
                            break;
                            
                        case Type t when t == typeof(double?):
                            // For double properties, we can directly set the value
                            var dblValue2 = row.GetDouble(columnName);
                            CheckThrowIfNull(dblValue2, prop.PropertyType, columnName);
                            if (dblValue2.HasValue)
                                prop.SetValue(obj, dblValue2);
                            break;
                        // decimal
                        case Type t when t == typeof(decimal):
                            // For decimal properties, we can directly set the value
                            var decimalValue = row.GetDecimal(columnName);
                            CheckThrowIfNull(decimalValue, prop.PropertyType, columnName);
                            prop.SetValue(obj, decimalValue);
                            break;
                        case Type t when t == typeof(decimal?):
                            // For decimal properties, we can directly set the value
                            var decimalValue2 = row.GetDecimal(columnName);
                            CheckThrowIfNull(decimalValue2, prop.PropertyType, columnName);
                            if (decimalValue2.HasValue)
                                prop.SetValue(obj, decimalValue2);
                            break;
        
                        case Type t when t == typeof(bool):
                            // For bool properties, we can directly set the value
                            var boolValue = row.GetBool(columnName);
                            CheckThrowIfNull(boolValue, prop.PropertyType, columnName);
                            prop.SetValue(obj, boolValue);
                            break;

                        case Type t when t == typeof(bool?):
                            // For bool properties, we can directly set the value
                            var boolValue2 = row.GetBool(columnName);
                            if (boolValue2.HasValue)
                                prop.SetValue(obj, boolValue2);
                            break;

                        case Type t when t == typeof(DateTime):
                            // For DateTime properties, we can directly set the value
                            var dateTimeValue = row.GetDate(columnName);
                            CheckThrowIfNull(dateTimeValue, prop.PropertyType, columnName);
                            prop.SetValue(obj, dateTimeValue);
                            break;

                        case Type t when t == typeof(DateTime):
                            // For DateTime properties, we can directly set the value
                            var dateTimeValue2 = row.GetDate(columnName);
                            if (dateTimeValue2.HasValue)
                                prop.SetValue(obj, dateTimeValue2);
                            break;


                        case Type t when t.IsEnum:
                            // For Enum properties, we can directly set the value
                             var enumValue = row.GetEnum(columnName, prop.PropertyType);
                             CheckThrowIfNull(enumValue, prop.PropertyType, columnName);
                            if (enumValue != null)
                                prop.SetValue(obj, enumValue);
                            break;

                        default:
                            throw new NotImplementedException($"Property type {prop.PropertyType.Name} is not implemented for mapping column {columnName}.");
                    }
               
                }
            }
        }

        return obj;
    }


    
    private static void CheckThrowIfNull(object? value, Type type, string columnName)
    {
        if (value == null || value == DBNull.Value)
        {
            throw new ArgumentNullException(nameof(value), $"The value for type column {columnName} cannot be null or DBNull.");
        }
    }
}