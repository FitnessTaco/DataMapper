

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
                    if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        // Get the underlying type of the nullable type
                        var underlyingType = Nullable.GetUnderlyingType(prop.PropertyType); 
             
                        var value = row.GetValue<T>(columnName);
                        if (value != DBNull.Value)
                        {
                            prop.SetValue(obj, Convert.ChangeType(value, underlyingType));
                        }
                    }
                    else
                    {
                        // For non-nullable types, we can directly set the value
                        var value = row.GetValue(prop.PropertyType, columnName);
                        if (value != DBNull.Value)
                        {
                            prop.SetValue(obj, Convert.ChangeType(value, prop.PropertyType));
                        }
                    }
                    // get the type of the property
                    /*
                    var t = prop.PropertyType;
                    var value = row.GetValue<typeof(t)>(columnName);
                    //var value = row[columnName];
                    if (value != DBNull.Value)
                    {
                        prop.SetValue(obj, Convert.ChangeType(value, prop.PropertyType));
                    }
                    */
                }
            }
        }

        return obj;
    }
}