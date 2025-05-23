using System.Data;
using dottech.data;

namespace DataMapperTests;

[TestClass]
public sealed class Test1
{
    private DataTable GetSampleDataTable()
    {
        var table = new DataTable();
        table.Columns.Add("Id", typeof(int));
        table.Columns.Add("DateCreated", typeof(DateTime));
        table.Columns.Add("IsActivew", typeof(bool));
        return table;
    }

    [TestMethod]
    public void TestDataRowExtensions()
    {
        // build a data table
        var table = GetSampleDataTable();
        // add a row with data
        var value1 = 1;
        var value2 = DateTime.Now;
        var value3 = true;
        table.Rows.Add(value1, value2, value3);


        Assert.AreEqual(value1.ToString(), table.Rows[0].GetString("Id"));
        Assert.AreEqual(value1, table.Rows[0].GetInt("Id"));
        Assert.AreEqual(value2, table.Rows[0].GetDate("DateCreated"));
        Assert.AreEqual(value3, table.Rows[0].GetBool("IsActivew"));

    }

    [TestMethod]
    public void TestDataRowExtensionsNullValues()
    {
        // build a data table
        var table = GetSampleDataTable();
        // add a row with data
        table.Rows.Add(null, null, null);

        var value = table.Rows[0].GetInt("Id");
        Assert.IsNull(table.Rows[0].GetInt("Id"));
        Assert.IsNull(table.Rows[0].GetDate("DateCreated"));
        Assert.IsNull(table.Rows[0].GetBool("IsActivew"));
        // null strings should return empty string
        Assert.AreEqual(string.Empty, table.Rows[0].GetString("Id"));
    }
    
    
    [TestMethod]
    public void TestDataRowExtensionsDefaultNullValues()
    {
        // build a data table
        var table = GetSampleDataTable();
        // add a row with data
        table.Rows.Add(null, null, null);

        Assert.AreEqual(default(int), table.Rows[0].GetIntOrDefault("Id"));
        Assert.AreEqual(5, table.Rows[0].GetIntOrDefault("Id", 5));
        
    }
}
