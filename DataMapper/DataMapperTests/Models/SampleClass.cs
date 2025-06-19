using DataMapper;

[MapToTable(TableName)]
public class SampleClass
{
    public const string TableName = "SampleTable";

    [MapTo("Id")]
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
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