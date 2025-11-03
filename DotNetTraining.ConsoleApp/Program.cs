// See https://aka.ms/new-console-template for more information
using System.Data;
using System.Data.SqlClient;

Console.WriteLine("Hello, World!");

string connectionString = "Data Source=DESKTOP-UUEE2ME\\SQLEXPRESS;Initial Catalog=DotNetTraining5;User ID=sa;Password=17112006";
SqlConnection connection = new SqlConnection(connectionString);
connection.Open();
Console.WriteLine("Connection is opened");

string query = @"SELECT [Blog_id]
      ,[Blog_title]
      ,[Blog_author]
      ,[Blog_context]
  FROM [dbo].[blogTable]";
SqlCommand cmd = new SqlCommand(query, connection);

// for each looping (Data reading after closing connection) //
SqlDataAdapter adapter = new SqlDataAdapter(cmd);
DataTable dt = new DataTable();
adapter.Fill(dt);

connection.Close();
Console.WriteLine("Connection is closed");

foreach (DataRow dr in dt.Rows)
{
    Console.WriteLine(dr["Blog_title"]);
    Console.WriteLine(dr["Blog_author"]);
    Console.WriteLine(dr["Blog_context"]);
}

Console.ReadKey();