// See https://aka.ms/new-console-template for more information


using BStorm.Tools.Database;
using Microsoft.Data.SqlClient;

const string CONNECTION_STRING = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DemoAdvancedSecurity;User Id=AppLogin; Password=Test1234=;Connect Timeout=60;Encrypt=True;Trust Server Certificate=True;";

using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
{
    connection.Open();

    var result = connection.ExecuteReader("AppSchema.GetValues", dr => new { Id = (int)dr["Id"], Value = (string)dr["Value"] }, true);

    foreach(var item in result)
    {
        Console.WriteLine(item);
    }
}