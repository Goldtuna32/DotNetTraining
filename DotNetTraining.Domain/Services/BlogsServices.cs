using DotNetTrainingDatabase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTraining.Domain.Services
{
    public class BlogsServices : IBlogsServices
    {
        private readonly AppDbContext _db;

        public BlogsServices(AppDbContext db)
        {
            _db = db;
        }

        public List<BlogTable> Blogs()
        {
            var model = _db.BlogTables.AsNoTracking().ToList();
            return model;
        }

        public List<BlogTable> GetDeletedBlogs()
        {
            var model = _db.BlogTables
                .AsNoTracking()
                .Where(x => x.DeleteFlag == true)
                .ToList();
            return model;
        }

        public void CreateBlog(BlogTable blog)
        {
            _db.BlogTables.Add(blog);
            _db.SaveChanges();
        }

        public BlogTable GetBlogById(int id)
        {
            var item = _db.BlogTables
            .AsNoTracking()
            .FirstOrDefault(x => x.BlogId == id && x.DeleteFlag == false);
            if (item is null)
            {
                return null;
            }
            return item;
        }

        public BlogTable EditBlog(int id, BlogTable blogsDTO)
        {
            var item = _db.BlogTables
            .AsNoTracking()
            .FirstOrDefault(x => x.BlogId == id && x.DeleteFlag == false);
            if (item is null)
            {
                return null;
            }
            item.BlogTitle = blogsDTO.BlogTitle;
            item.BlogAuthor = blogsDTO.BlogAuthor;
            item.BlogContext = blogsDTO.BlogContext;
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return item;
        }

        public BlogTable UpdateBlog(int id, BlogTable blogsDTO)
        {
            var item = _db.BlogTables
            .AsNoTracking()
            .FirstOrDefault(x => x.BlogId == id && x.DeleteFlag == false);
            if (item is null)
            {
                return null;
            }
            if (!string.IsNullOrEmpty(blogsDTO.BlogTitle))
            {
                item.BlogTitle = blogsDTO.BlogTitle;
            }
            if (!string.IsNullOrEmpty(blogsDTO.BlogAuthor))
            {
                item.BlogAuthor = blogsDTO.BlogAuthor;
            }
            if (!string.IsNullOrEmpty(blogsDTO.BlogContext))
            {
                item.BlogContext = blogsDTO.BlogContext;
            }
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return item;
        }

        public bool DeleteBlog(int id)
        {
            var item = _db.BlogTables
            .AsNoTracking()
            .FirstOrDefault(x => x.BlogId == id && x.DeleteFlag == false);
            if (item is null)
            {
                return false;
            }
            _db.Entry(item).State = EntityState.Deleted;
            _db.SaveChanges();
            return true;
        }

        public bool RestoreBlog(int id)
        {
            var item = _db.BlogTables
            .AsNoTracking()
            .FirstOrDefault(x => x.BlogId == id && x.DeleteFlag == true);
            if (item is null)
            {
                return false;
            }
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return true;
        }

    }
}
