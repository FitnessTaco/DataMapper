

using DataMapper;
using dottech.data;

[MapToTable(TableName)]
public class SampleClass
{
    public const string TableName = "SampleTable";

    [MapTo("Id")]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}