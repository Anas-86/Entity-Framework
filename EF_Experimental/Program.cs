using EF_Experimental;
using Microsoft.Data.SqlClient;
using System;

// Check if the connection is working
string connectionString = "Server=localhost;Database=Northwind;User Id=sa;Password=Hitman4719781978;";
using (var connection = new SqlConnection(connectionString))
{
    try
    {
        connection.Open();
        Console.WriteLine("Connection successful!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Connection failed: {ex.Message}");
    }
}

QueryingCategories();
FilteredIncludes();
QueryingProducts();