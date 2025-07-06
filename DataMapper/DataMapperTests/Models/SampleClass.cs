using DataMapper;

[MapToTable(TableName)]
public class SampleClass
{
    public const string TableName = "SampleTable";

    [MapTo("Id")]
    public int Id { get; set; }
    [MapTo("IsActive")]
    public bool IsActive { get; set; }
    [MapTo("Name")]
    public string? Name { get; set; }
    [MapTo("Description")]
    public string? Description { get; set; }
    [MapTo("CreatedAt")]
    public DateTime CreatedAt { get; set; }
    [MapTo("UpdatedAt")]
    public DateTime UpdatedAt { get; set; }

    [MapTo("SampleEnum")]
    public SampleEnum SampleEnumValue { get; set; }
}

public enum SampleEnum
{
    Value1,
    Value2,
    Value3
}