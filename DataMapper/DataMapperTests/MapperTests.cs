using System.Data;
using dottech.data;
namespace DataMapperTests;

[TestClass]
public sealed class MapperTests
{

    private DataTable GetSampleDataTable()
    {
        var table = new DataTable();
        table.Columns.Add("Id", typeof(int));
        table.Columns.Add("IsActive", typeof(bool));
        table.Columns.Add("Name", typeof(string));
        table.Columns.Add("Description", typeof(string));
        table.Columns.Add("SampleEnum", typeof(SampleEnum));
        table.Columns.Add("CreatedAt", typeof(DateTime));
        table.Columns.Add("UpdatedAt", typeof(DateTime));
        return table;
    }

    [TestMethod]
    public void TestMapper()
    {
        // build a data table
        var table = GetSampleDataTable();
        // add a row with data
        var value1 = 1;
        var value2 = true;
        var value3 = "Sample Name";
        var value4 = "Sample Description";
        var value5 = SampleEnum.Value2;
        var value6 = DateTime.Now.AddDays(-1);
        var value7 = DateTime.Now;
        table.Rows.Add(value1, value2, value3, value4, value5, value6, value7);

        // map the row to an object
        var obj = Mapper.Map<SampleClass>(table.Rows[0]);
        Assert.AreEqual(value1, obj.Id);
        Assert.AreEqual(value2, obj.IsActive);
        Assert.AreEqual(value3, obj.Name);
        Assert.AreEqual(value4, obj.Description);
        Assert.AreEqual(value5, obj.SampleEnumValue);
        Assert.AreEqual(value6, obj.CreatedAt);
        Assert.AreEqual(value7, obj.UpdatedAt);
    }
}