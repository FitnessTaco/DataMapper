using System;
using DataMapper;

[MapToTable(DataIgnore.TableName)]
public class DataIgnore
{
    public const string TableName = "DataIgnore";

    [MapTo("Id")]
    public int Id { get; set; }
    [DataIgnoreMap]
    public string IgnoredProperty { get; set; } = "This property should be ignored during mapping";



}
