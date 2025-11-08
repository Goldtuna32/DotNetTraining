using Dapper;
using DotNetTraining.ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTraining.ConsoleApp
{
    public class DapperExampleTesting
    {
        private readonly string _connectionString = "Data Source=DESKTOP-UUEE2ME\\SQLEXPRESS;Initial Catalog=DotNetTraining5;User ID=sa;Password=17112006";

        public void Read()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "Select * from blog_table where Delete_flag = 0";
                var lst = db.Query<BlogDataModel>(query).ToList();
                foreach (var items in lst)
                {
                    Console.WriteLine(items.Blog_id);
                    Console.WriteLine(items.Blog_title);
                    Console.WriteLine(items.Blog_author);
                    Console.WriteLine(items.Blog_context);
                }
            }
        }

        public void Create(string title, string author, string context)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
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
                int result = db.Execute(query, new BlogDataModel { Blog_title = title, Blog_author = author, Blog_context = context });
                Console.WriteLine(result == 1 ? "Insert successful" : "insert failed");
            }
        }

        public void Edit(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = @"SELECT [Blog_id]
      ,[Blog_title]
      ,[Blog_author]
      ,[Blog_context]
      ,[Delete_flag]
  FROM [dbo].[blogTable] where Blog_id = @Blog_id and Delete_flag = 0";
                var item = db.Query<BlogDataModel>(query, new BlogDataModel { Blog_id = id }).FirstOrDefault();
                if (item is null)
                {
                    Console.WriteLine("Record not found");
                    return;
                }
                Console.WriteLine(item.Blog_id);
                Console.WriteLine(item.Blog_title);
                Console.WriteLine(item.Blog_author);
                Console.WriteLine(item.Blog_context);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                if (id is 0)
                {
                    Console.WriteLine("Record not found");
                    return;
                }
                string query = "Update blogTable set Delete_flag = 1 where Blog_id = @Blog_id";
                int result = db.Execute(query, new BlogDataModel { Blog_id = id });
                Console.WriteLine(result == 1 ? "Delete successful" : "Delete failed");
            }
        }

        public void Restore(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                if (id is 0)
                {
                    Console.WriteLine("Record not found");
                    return;
                }
                string query = "Update blogTable set Delete_flag = 0 where Blog_id = @Blog_id";
                int result = db.Execute(query, new BlogDataModel { Blog_id = id });
                Console.WriteLine(result == 1 ? "Restore successful" : "Restore failed");
            }
        }

        public void ReadDeleted()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "Select * from blog_table where Delete_flag = 1";
                var lst = db.Query<BlogDataModel>(query).ToList();
                foreach (var items in lst)
                {
                    Console.WriteLine(items.Blog_id);
                    Console.WriteLine(items.Blog_title);
                    Console.WriteLine(items.Blog_author);
                    Console.WriteLine(items.Blog_context);
                }
            }
        }

        public void Update(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string readQuery = @"SELECT [Blog_id]
      ,[Blog_title]
      ,[Blog_author]
      ,[Blog_context]
      ,[Delete_flag]
  FROM [dbo].[blogTable] where Blog_id = @Blog_id and Delete_flag = 0";
                var item = db.Query<BlogDataModel>(readQuery, new BlogDataModel { Blog_id = id }).FirstOrDefault();
                if (item is null)
                {
                    Console.WriteLine("Record not found");
                    return;
                }

                Console.WriteLine("Enter new title:");
                string newTitle = Console.ReadLine();
                

                Console.WriteLine("Enter new Author:");
                string newAuthor = Console.ReadLine();
               
                Console.WriteLine("Enter new Context:");
                string newContext = Console.ReadLine();
               

                string updateQuery = @"UPDATE [dbo].[blogTable]
   SET [Blog_title] = @Blog_title
      ,[Blog_author] = @Blog_author 
      ,[Blog_context] = @Blog_context
      ,[Delete_flag] = 0
 WHERE [Blog_id] = @Blog_id";
                
                List<BlogDataModel> updatedList = new List<BlogDataModel>();
                foreach (var items in updatedList)
                {
                    items.Blog_title = newTitle;
                    if (string.IsNullOrEmpty(newTitle))
                        newTitle = items.Blog_title;

                    items.Blog_author = newAuthor;
                    if (string.IsNullOrEmpty(newAuthor))
                        newAuthor = items.Blog_author;

                    items.Blog_context = newContext;
                    if (string.IsNullOrEmpty(newContext))
                        newContext = items.Blog_context;

                    updatedList.Add(items);
                }

                int result = db.Execute(updateQuery, updatedList);
                Console.WriteLine(result == 1 ? "Update successful" : "Update failed");
            }
        }
    }
}
