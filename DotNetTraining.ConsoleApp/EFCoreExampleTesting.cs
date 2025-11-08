using DotNetTraining.ConsoleApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTraining.ConsoleApp
{
    public class EFCoreExampleTesting
    {
        public void Read()
        {
            AppDbContext db = new AppDbContext();
            var list = db.Blogs.Where(x => x.Delete_flag == false).ToList();
            foreach (var item in list)
            {
                Console.WriteLine(item.Blog_id);
                Console.WriteLine(item.Blog_title);
                Console.WriteLine(item.Blog_author);
                Console.WriteLine(item.Blog_context);
            }
        }

        public void Create(string title, string author, string context)
        {
            BlogDataModel blogs = new BlogDataModel
            {
                Blog_title = title,
                Blog_author = author,
                Blog_context = context
            };
            AppDbContext db = new AppDbContext();
            db.Blogs.Add(blogs);
            var result = db.SaveChanges();
            Console.WriteLine(result == 1 ? "Creation Successful" : "Creation Failed");
        }

        public void Edit(int id)
        {
            AppDbContext db = new AppDbContext();
            var item = db.Blogs.FirstOrDefault(x => x.Blog_id == id && x.Delete_flag == false);
            if (item is null)
            {
                Console.WriteLine("No record found");
                return;
            }
            Console.WriteLine(item.Blog_id);
            Console.WriteLine(item.Blog_title);
            Console.WriteLine(item.Blog_author);
            Console.WriteLine(item.Blog_context);
        }

        public void Update(int id, string title, string author, string context)
        {
            AppDbContext db = new AppDbContext();
            var item = db.Blogs.AsNoTracking().FirstOrDefault(x => x.Blog_id == id && x.Delete_flag == false);
            if (item is null)
            {
                Console.WriteLine("No Record found");
                return;
            }
            Console.WriteLine(item.Blog_id);
            Console.WriteLine(item.Blog_title);
            Console.WriteLine(item.Blog_author);
            Console.WriteLine(item.Blog_context);

            if (!string.IsNullOrEmpty(title))
            { item.Blog_title = title; }

            if (!string.IsNullOrEmpty(author))
            { item.Blog_author = author; }

            if (!string.IsNullOrEmpty(context))
            { item.Blog_context = context; }

            db.Entry(item).State = EntityState.Modified;
            var result = db.SaveChanges();
            Console.WriteLine(result == 1 ? "Updation Successful" : "Updating Failed");

        }

        public void Delete(int id)
        {
            AppDbContext db = new AppDbContext();
            var item = db.Blogs.AsNoTracking().FirstOrDefault(x => x.Blog_id == id);
            if (item is null)
            {
                Console.WriteLine("Recorded Not Found");
                return;
            }
            item.Delete_flag = true;
            db.Entry(item).State = EntityState.Deleted;
            var result = db.SaveChanges();
            Console.WriteLine(result == 1 ? "Delete Successful" : "Deleting Failed");
        }

        public void Restore(int id)
        {
            AppDbContext db = new AppDbContext();
            var item = db.Blogs.AsNoTracking().FirstOrDefault(x => x.Blog_id == id && x.Delete_flag == true);
            if (item is null)
            {
                Console.WriteLine("No Record Found");
                return;
            }
            db.Entry(item).State = EntityState.Modified;
            var result = db.SaveChanges();
            Console.WriteLine(result == 1 ? "Restoration Successful" : "Restoration Failed");
        }

        }
    }
