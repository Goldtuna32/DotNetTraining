using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTraining.ConsoleApp
{
    public class AdoDotNetSample
    {
        private readonly string _connectionString = "Data Source=DESKTOP-UUEE2ME\\SQLEXPRESS;Initial Catalog=DotNetTraining5;User ID=sa;Password=17112006";

        public void Read()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            Console.WriteLine("Connection is opened");
            string query = @"SELECT [Blog_id]
      ,[Blog_title]
      ,[Blog_author]
      ,[Blog_context]
      ,[Delete_flag]
  FROM [dbo].[blogTable] where [Delete_flag] = 1";
            SqlCommand cmd = new SqlCommand(query, connection);

            ////SQL Reader //
            //SqlDataReader reader = cmd.ExecuteReader();
            //while (reader.Read())
            //{
            //    Console.WriteLine(reader["Blog_title"]);
            //    Console.WriteLine(reader["Blog_author"]);
            //    Console.WriteLine(reader["Blog_context"]);
            //}

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
                Console.WriteLine(dr["Delete_flag"]);
            }

        }

        public void Create() {
            SqlConnection connection1 = new SqlConnection(_connectionString);
            connection1.Open();
            Console.WriteLine("Connection1 is opened");
            Console.Write("Enter Blog Title :");
            String Title = Console.ReadLine();
            Console.Write("Enter Blog Author :");
            String Author = Console.ReadLine();
            Console.Write("Enter Blog Context :");
            String Context = Console.ReadLine();

            //avoid using this because of sql injection can cause table to drop
            //string query1 = $@"INSERT INTO [dbo].[blogTable]
            //           ([Blog_title]
            //           ,[Blog_author]
            //           ,[Blog_context])
            //     VALUES
            //           ('{Title}'
            //           ,'{Author}'
            //           ,'{Context}')";

            string query1 = $@"INSERT INTO [dbo].[blogTable]
           ([Blog_title]
           ,[Blog_author]
           ,[Blog_context]
           ,[Delete_flag])
     VALUES
           (@Blog_title
           ,@Blog_author
           ,@Blog_context
           ,@Delete_flag, 1)";
            SqlCommand cmd1 = new SqlCommand(query1, connection1);
            cmd1.Parameters.AddWithValue("@Blog_title", Title);
            cmd1.Parameters.AddWithValue("@Blog_author", Author);
            cmd1.Parameters.AddWithValue("@Blog_context", Context);
            int result = cmd1.ExecuteNonQuery();

            //Ternary Operator (How it work is : value == 1 ? "Success Message"  : "Failed Message"); 
            Console.WriteLine(result == 1 ? "Saving Successful" : "Saving Failed");

            connection1.Close();
            Console.WriteLine("Connection1 is closed");

        }

        public string GetById(string id)
        {
            //Console.Write("Enter Blog ID: ");
            //id = Console.ReadLine();
            try
            {
                SqlConnection connection = new SqlConnection(_connectionString);
                connection.Open();
                string query = $@"SELECT [Blog_id]
      ,[Blog_title]
      ,[Blog_author]
      ,[Blog_context]
      ,[Delete_flag]
  FROM [dbo].[blogTable] where [Blog_id] = @Blog_id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Blog_id", id);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                //SqlDataReader reader = cmd.ExecuteReader();
                //while (reader.Read())
                //{
                //    Console.WriteLine(reader["Blog_id"]);
                //    Console.WriteLine(reader["Blog_title"]);
                //    Console.WriteLine(reader["Blog_author"]);
                //    Console.WriteLine(reader["Blog_context"]);
                //}
                connection.Close();
            }
            catch (Exception e)
            {

                Console.WriteLine("Id must not be null here");
            }
            return id;

        }

        public void Edit()
        {
            Console.Write("Enter Blog ID to Edit: ");
            string id = Console.ReadLine();

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"SELECT [Blog_id]
      ,[Blog_title]
      ,[Blog_author]
      ,[Blog_context]
      ,[Delete_flag]
  FROM [dbo].[blogTable] where [Blog_id] = @Blog_id";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            cmd.Parameters.AddWithValue("@Blog_id", id);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count == 0)
            {
                Console.WriteLine("Blog not found.");
                return;
            } 
            DataRow dr = dt.Rows[0];
            Console.WriteLine(dr["Blog_title"]);
            Console.WriteLine(dr["Blog_author"]);
            Console.WriteLine(dr["Blog_context"]);

        }

        public void Update()
        {
            Console.Write("Enter Blog ID to Update: ");
            string id = Console.ReadLine();
            if (id.Length == 0)
            {
                Console.WriteLine("ID cannot be empty.");
                return;
            }

            Console.Write("Enter Blog Title :");
            String Title = Console.ReadLine();
            if (Title.Length == 0)
            {
                Console.WriteLine("Title cannot be empty.");
                return;
            }

            Console.Write("Enter Blog Author :");
            String Author = Console.ReadLine();
            if (Author.Length == 0)
            {
                Console.WriteLine("Author cannot be empty.");
                return;
            }

            Console.Write("Enter Blog Context :");
            String Context = Console.ReadLine();
            if (Context.Length == 0)
            {
                Console.WriteLine("Context cannot be empty.");
                return;
            }

            SqlConnection connection1 = new SqlConnection(_connectionString);
            connection1.Open();

            string query1 = $@"UPDATE [dbo].[blogTable]
   SET [Blog_title] = @Blog_title 
      ,[Blog_author] = @Blog_author
      ,[Blog_context] = @Blog_context 
      ,[Delete_flag] = 0";
            SqlCommand cmd1 = new SqlCommand(query1, connection1);
            cmd1.Parameters.AddWithValue("@Blog_id", id);
            cmd1.Parameters.AddWithValue("@Blog_title", Title);
            cmd1.Parameters.AddWithValue("@Blog_author", Author);
            cmd1.Parameters.AddWithValue("@Blog_context", Context);
            int result = cmd1.ExecuteNonQuery();

            //Ternary Operator (How it work is : value == 1 ? "Success Message"  : "Failed Message"); 
            Console.WriteLine(result == 1 ? "Updating Successful" : "Updating Failed");

            connection1.Close();
            Console.WriteLine("Connection1 is closed");

        }

        public void Delete()
        {
            Console.Write("Enter Blog ID to Delete: ");
            string id = Console.ReadLine();
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            string query = $@"UPDATE [dbo].[blogTable]
    SET [Delete_flag] = 0 where [Blog_id] = @Blog_id";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Blog_id", id);
            int result = cmd.ExecuteNonQuery();
            //Ternary Operator (How it work is : value == 1 ? "Success Message"  : "Failed Message"); 
            Console.WriteLine(result == 1 ? "Deletion Successful" : "Deletion Failed");
            connection.Close();
            Console.WriteLine("Connection is closed");
        }

        public void RestoreDelete()
        {
            Console.Write("Enter Blog ID to Restore: ");
            string id = Console.ReadLine();
            if (id.Length == 0)
            {
                Console.WriteLine("Blog Id can't be empty");
                return;
            }
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            string query = $@"UPDATE [dbo].[blogTable]
    SET [Delete_flag] = 1 where [Blog_id] = @Blog_id";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Blog_id", id);
            int result = cmd.ExecuteNonQuery();
            //Ternary Operator (How it work is : value == 1 ? "Success Message"  : "Failed Message"); 
            Console.WriteLine(result == 1 ? "Restoration Successful" : "Restoration Failed");
            connection.Close();
            Console.WriteLine("Connection is closed");
        }

        public void ReadDelete()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            string query = @"SELECT [Blog_id]
      ,[Blog_title]
      ,[Blog_author]
      ,[Blog_context]
      ,[Delete_flag]
  FROM [dbo].[blogTable] where [Delete_flag] = 0";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();

            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine(dr["Blog_id"]);
                Console.WriteLine(dr["Blog_title"]);
                Console.WriteLine(dr["Blog_author"]);
                Console.WriteLine(dr["Blog_context"]);
            }
        }
    }
}
