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
    public class DapperExample
    {
        private readonly string _connectionString = "Data Source=DESKTOP-UUEE2ME\\SQLEXPRESS;Initial Catalog=DotNetTraining5;User ID=sa;Password=17112006";

        public void Read()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "select * FROM blogTable where Delete_flag = 0;";
                var lst = db.Query<BlogDataModel>(query).ToList();
                foreach (var item in lst)
                {
                    Console.WriteLine(item.Blog_id);
                    Console.WriteLine(item.Blog_title);
                    Console.WriteLine(item.Blog_author);
                    Console.WriteLine(item.Blog_context);
                }
            }
        }

        public void Create(string title, string author, string context)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query1 = $@"INSERT INTO [dbo].[blogTable]
           ([Blog_title]
           ,[Blog_author]
           ,[Blog_context]
           ,[Delete_flag])
     VALUES
           (@Blog_title
           ,@Blog_author
           ,@Blog_context
           ,1)";
                int result = db.Execute(query1, new BlogDataModel { Blog_title = title, Blog_author = author, Blog_context = context });

                Console.WriteLine(result == 1 ? "Insert successful" : "Insert failed");
            }
        }

        public void Edit(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = @"Select * FROM blogTable where Delete_flag = 1 and Blog_id = @Blog_id;";
                var item = db.Query<BlogDataModel>(query, new BlogDataModel
                {
                    Blog_id = id
                }).FirstOrDefault();

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

                string query = @"UPDATE [dbo].[blogTable] set [Delete_flag] = 1
where [Blog_id] = @Blog_id";
                var item = db.Query<BlogDataModel>(query, new BlogDataModel { Blog_id = id }).FirstOrDefault();
                int result = db.Execute(query, new BlogDataModel
                {
                    Blog_id = id
                });
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

                string query = @"UPDATE blogTable set Delete_flag = 0 where Blog_id = @Blog_id";
                int result = db.Execute(query, new BlogDataModel
                {
                    Blog_id = id
                });
                Console.WriteLine(result == 1 ? "Restore successful" : "Restore failed");
            }
        }

        public void Update(int id)
        {
            if (id is 0)
            {
                Console.WriteLine("Record not found");
                return;
            }

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string readQuery = @"SELECT [Blog_id]
      ,[Blog_title]
      ,[Blog_author]
      ,[Blog_context]
      ,[Delete_flag]
  FROM [dbo].[blogTable] where Blog_id = @Blog_id and Delete_flag = 1";
                var list = db.Query<BlogDataModel>(readQuery, new BlogDataModel { Blog_id = id });
                foreach (var item in list)
                {
                    Console.WriteLine(item.Blog_id);
                    Console.WriteLine(item.Blog_title);
                    Console.WriteLine(item.Blog_author);
                    Console.WriteLine(item.Blog_context);
                }

                Console.WriteLine("Enter Updated Blog Title: ");
                string title = Console.ReadLine();

                Console.WriteLine("Enter Updated Blog Author: ");
                string author = Console.ReadLine();
                Console.WriteLine("Enter Updated Blog Context: ");
                string context = Console.ReadLine();


                string query = @"UPDATE [dbo].[blogTable]
   SET [Blog_title] = @Blog_title
      ,[Blog_author] = @Blog_author 
      ,[Blog_context] = @Blog_context
      ,[Delete_flag] = 1
 WHERE [Blog_id] = @Blog_id";
                List<BlogDataModel> updatedList = new List<BlogDataModel>();
                foreach (var item in list)
                {
                    item.Blog_title = title;
                    if (string.IsNullOrEmpty(title))
                        title = item.Blog_title;

                    if (string.IsNullOrEmpty(author))
                        author = item.Blog_author;
                    item.Blog_author = author;

                    if (string.IsNullOrEmpty(context))
                        context = item.Blog_context;
                    item.Blog_context = context;

                    updatedList.Add(item);
                }
                int result = db.Execute(query, updatedList);
                Console.WriteLine(result == 1 ? "Update successful" : "Update failed");
            }
        }

        public void DeletedList()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "select * FROM blogTable where Delete_flag = 1;";
                var lst = db.Query<BlogDataModel>(query).ToList();
                foreach (var item in lst)
                {
                    Console.WriteLine(item.Blog_id);
                    Console.WriteLine(item.Blog_title);
                    Console.WriteLine(item.Blog_author);
                    Console.WriteLine(item.Blog_context);
                }
            }
        }
    }
}
