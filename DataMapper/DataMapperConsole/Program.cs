// See https://aka.ms/new-console-template for more information
using System.Data;
using dottech.data;

Console.WriteLine("Hello, World!");

// create a new System.Data.DataTable object with three columns
var table = new DataTable();
table.Columns.Add("Id", typeof(int));
table.Columns.Add("DateCreated", typeof(DateTime));
table.Columns.Add("IsActivew", typeof(bool));
table.Rows.Add(1, DateTime.Now, true);    

var result = table.Rows[0].GetString("Id");
Console.WriteLine($"GetString result: {result}");
Console.WriteLine($"GetInt result: {table.Rows[0].GetInt("Id")}");
Console.WriteLine($"GetDouble result: {table.Rows[0].GetDouble("Id")}");
Console.WriteLine($"GetDecimal result: {table.Rows[0].GetDecimal("Id")}");
Console.WriteLine($"DateCreated result: {table.Rows[0].GetDate("DateCreated")}");
Console.WriteLine($"GetBool result: {table.Rows[0].GetBool("IsActivew")}");