using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTraining.ConsoleApp
{
    public class AdoDotNetSampleTesting
    {
        private readonly string _connectionString = "Data Source=DESKTOP-UUEE2ME\\SQLEXPRESS;Initial Catalog=DotNetTraining5;User ID=sa;Password=17112006";

        public void Read()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            try
            {
                connection.Open();
            }
            catch (SqlException)
            {

                Console.WriteLine("Connection is failed to open");
            }
            string query = @"SELECT [Blog_id]
      ,[Blog_title]
      ,[Blog_author]
      ,[Blog_context]
      ,[Delete_flag]
  FROM [dbo].[blogTable] where [Delete_flag] = 1";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();
            foreach(DataRow dr in dt.Rows)
            {

                Console.WriteLine(dr["Blog_id"]);
                Console.WriteLine(dr["Blog_title"]);
                Console.WriteLine(dr["Blog_author"]);
                Console.WriteLine(dr["Blog_context"]); 
            }


        }

        public void Create()
        {
            Console.WriteLine("Enter Blog Title:");
            String Title = Console.ReadLine();

            if (String.IsNullOrEmpty(Title))
            {
                Console.WriteLine("Title cannot be empty");
                return;
            }
            

            Console.WriteLine("Enter Blog Author:");
            String Author = Console.ReadLine();

            if (String.IsNullOrEmpty(Author))
                        {
                            Console.WriteLine("Author cannot be empty");
                            return;
                        }
            Console.WriteLine("Enter Blog Context:");
            String Context = Console.ReadLine();

            if (String.IsNullOrEmpty(Context))
            {
                Console.WriteLine("Context cannot be empty");
                return;
            }

            SqlConnection connection = new SqlConnection(_connectionString);
            try
            {
                connection.Open();
            }
            catch (SqlException)
            {
                Console.WriteLine("Connection is failed to open");
            }
            string query = @"INSERT INTO [dbo].[blogTable]
           ([Blog_title]
           ,[Blog_author]
           ,[Blog_context]
           ,[Delete_flag])
     VALUES
           (@Blog_title
           ,@Blog_author
           ,@Blog_context
           ,1)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Blog_title", Title);
            cmd.Parameters.AddWithValue("@Blog_author", Author);
            cmd.Parameters.AddWithValue("@Blog_context", Context);
            int result = cmd.ExecuteNonQuery();
            Console.WriteLine(result == 1 ? "Saving Successful" : "Saving Failed");
            connection.Close();
        }

        public void GetById()
        {
            Console.WriteLine("Enter Blog Id:");
            string id = Console.ReadLine();

            if (string.IsNullOrEmpty(id))
            {
                Console.WriteLine("Id cannot be empty");
                return;
            }

            SqlConnection connection = new SqlConnection(_connectionString);
            try
            {
                connection.Open();
            }
            catch (SqlException)
            {
                Console.WriteLine("Connection is failed to open");
            }
            string query = @"SELECT [Blog_id]
      ,[Blog_title]
      ,[Blog_author]
      ,[Blog_context]
      ,[Delete_flag]
  FROM [dbo].[blogTable] where [Blog_id] = @Blog_id and [Delete_flag] = 1";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Blog_id", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            if(dt.Rows.Count == 0)
            {
                Console.WriteLine("No Records Found");
            }

            connection.Close();

            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine(dr["Blog_id"]);
                Console.WriteLine(dr["Blog_title"]);
                Console.WriteLine(dr["Blog_author"]);
                Console.WriteLine(dr["Blog_context"]);
            }
        }

        public void Update()
        {
            Console.Write("Enter Blog Id: ");
            String id = Console.ReadLine();

            if (string.IsNullOrEmpty(id))
            {
                Console.WriteLine("Id cannot be empty");
                return;
            }

            Console.Write("Enter Blog Title to Update: ");
            String Title = Console.ReadLine();

            if (string.IsNullOrEmpty(Title))
            {
                Console.WriteLine("Title cannot be empty");
                return;
            }

            Console.Write("Enter Blog Author to Update: ");
            String Author = Console.ReadLine();

            if (string.IsNullOrEmpty(Author))
            {
                Console.WriteLine("Author cannot be empty");
                return;
            }

            Console.Write("Enter Blog Context to Update: ");
            String Context = Console.ReadLine();

            if (string.IsNullOrEmpty(Context))
            {
                Console.WriteLine("Context cannot be empty");
                return;
            }

            SqlConnection connection = new SqlConnection(_connectionString);
            try
            {
                connection.Open();
            }
            catch (SqlException)
            {
                Console.WriteLine("Connection is failed to open");
            }

            string query = @"UPDATE [dbo].[blogTable]
   SET [Blog_title] = @Blog_title
      ,[Blog_author] = @Blog_author 
      ,[Blog_context] = @Blog_context
      ,[Delete_flag] = 1
 WHERE [Blog_id] = @Blog_id";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Blog_id", id);
            cmd.Parameters.AddWithValue("@Blog_title", Title);
            cmd.Parameters.AddWithValue("@Blog_author", Author);
            cmd.Parameters.AddWithValue("@Blog_context", Context);

            int result = cmd.ExecuteNonQuery();
            Console.WriteLine(result == 1 ? "Update Successful" : "Update Failed");
        }

        public void Delete()
        {
            Console.Write("Enter Blog Id to Delete: ");
            String id = Console.ReadLine();

            if (string.IsNullOrEmpty(id))
            {
                Console.WriteLine("Id cannot be empty");
                return;
            }

            SqlConnection connection = new SqlConnection(_connectionString);
            try
            {
                connection.Open();
            }
            catch (SqlException)
            {
                Console.WriteLine("Connection is failed to open");
            }

            string query = @"UPDATE [dbo].[blogTable] set [Delete_flag] = 0
where [Blog_id] = @Blog_id";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Blog_id", id);

            int result = cmd.ExecuteNonQuery();
            Console.WriteLine(result == 1 ? "Deletion Successful" : "Deletion Failed");
        }

        public void ReadDeleted()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            try
            {
                connection.Open();
            }
            catch (SqlException)
            {
                Console.WriteLine("Connection is failed to open");
            }
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

            if(dt.Rows.Count == 0)
            {
                Console.WriteLine("No Deleted Records Found");
            }

            connection.Close();

            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine(dr["Blog_id"]);
                Console.WriteLine(dr["Blog_title"]);
                Console.WriteLine(dr["Blog_author"]);
                Console.WriteLine(dr["Blog_context"]);
            }
        }

        public void Restore()
        {
            Console.Write("Enter Blog Id to Restore: ");
            String id = Console.ReadLine();

            if (string.IsNullOrEmpty(id))
            {
                Console.WriteLine("Id cannot be empty");
                return;
            }
            SqlConnection connection = new SqlConnection(_connectionString);
            try
            {
                connection.Open();
            }
            catch (SqlException)
            {
                Console.WriteLine("Connection is failed to open");
            }
            string query = @"UPDATE [dbo].[blogTable] set [Delete_flag] = 1
where [Blog_id] = @Blog_id";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Blog_id", id);
            int result = cmd.ExecuteNonQuery();
            Console.WriteLine(result == 1 ? "Restoration Successful" : "Restoration Failed");
        }
    }
}
