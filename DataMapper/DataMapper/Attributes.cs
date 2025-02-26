using System;

namespace DataMapper;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class MapToTable : Attribute
    {
        public string TableName { get; }

        public MapToTable(string name)
        {
            TableName = name;
        }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class MapTo : Attribute
    {
        public string ColumnName { get; }

        public MapTo(string name)
        {
            ColumnName = name;
        }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class DataIgnoreMap : Attribute{}

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class PrimaryKey : Attribute{}

    
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class Identity : PrimaryKey{}