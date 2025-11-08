using DotNetTraining.ConsoleApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTraining.ConsoleApp
{
    public class EFCoreExample
    {
        public void Read()
        {
            AppDbContext db = new AppDbContext();
            var lst = db.Blogs.Where(x=> x.Delete_flag == true).ToList();
            foreach (var items in lst)
            {
                Console.WriteLine(items.Blog_id);
                Console.WriteLine(items.Blog_title);
                Console.WriteLine(items.Blog_author);
                Console.WriteLine(items.Blog_context);
            }

        }

        public void Create(string title, string author, string context)
        {
            BlogDataModel blogs = new BlogDataModel {
                Blog_title = title,
                Blog_author = author,
                Blog_context = context, 
            };
            AppDbContext db = new AppDbContext();
            db.Blogs.Add(blogs);
            var result =  db.SaveChanges();
            Console.WriteLine(result == 1 ? "Saving Successful" : "Saving failed");
        }

        public void Edit(int id)
        {
            AppDbContext db = new AppDbContext();
            var item = db.Blogs.FirstOrDefault(x=> x.Blog_id == id && x.Delete_flag == false);
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
            var item = db.Blogs.AsNoTracking().FirstOrDefault(x => x.Blog_id == id);
            if (item is null)
            {
                Console.WriteLine("No record found");
                return;
            }

            if (!string.IsNullOrEmpty(title))
            {
                item.Blog_title = title;
            }

            if (!string.IsNullOrEmpty(author))
            {
                item.Blog_author = author;
            }
            if (!string.IsNullOrEmpty(context))
            {
                item.Blog_context = context;
            }
            db.Entry(item).State = EntityState.Modified;
            var result = db.SaveChanges();
            Console.WriteLine(result == 1 ? "Updating Success" : "Update Failed");
        }

        public void Delete(int id)
        {
            AppDbContext db = new AppDbContext();
            var item = db.Blogs.AsNoTracking().FirstOrDefault(x => x.Blog_id == id && x.Delete_flag == false);
            if (item is null)
            {
                Console.WriteLine("No record found");
                return;
            }
            item.Delete_flag = true;
            db.Entry(item).State = EntityState.Deleted;
            var result = db.SaveChanges();
            Console.WriteLine(result == 1 ? "Deleted Successful" : "Deletion Failed");
        }
        
        public void RestoreDelete(int id)
        {
            AppDbContext db = new AppDbContext();
            var item = db.Blogs.AsNoTracking().FirstOrDefault(x => x.Blog_id == id && x.Delete_flag == true);
            if (item is null)
            {
                Console.WriteLine("No record found");
                return;
            }
            item.Delete_flag = false;
            db.Entry(item).State = EntityState.Modified;
            var result = db.SaveChanges();
            Console.WriteLine(result == 1 ? "Restoration Successful" : "Restoration Failed");
        }
    }
}
