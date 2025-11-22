using DotNetTrainingDatabase.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNetTraining.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsControllerExample : ControllerBase
    {
        private readonly AppDbContext _db = new AppDbContext();

        [HttpGet]
        public IActionResult GetBlogs()
        {
            var list = _db.BlogTables.ToList();
            if (list.Count == 0)
            {
                return NotFound("No blogs found.");
            }
            return Ok(list);
        }

        [HttpPost]
        public IActionResult CreateBlogs(BlogTable blogsTable)
        {
            _db.BlogTables.Add(blogsTable);
            _db.SaveChanges();
            return Ok();
        }
        [HttpGet("${id}")]
        public IActionResult GetBlogsById(int id)
        {
            var item = _db.BlogTables.AsNoTracking().FirstOrDefault(x => x.BlogId == id && x.DeleteFlag == false);
            if (item is null)
            {
                return NotFound("Blog not found.");
            }
            return Ok(item);
        }
        [HttpPut("${id}")]
        public IActionResult UpdateBlogs(int id, BlogTable blogTable)
        {
            var existingBlog = _db.BlogTables.AsNoTracking().FirstOrDefault(x => x.BlogId == id && x.DeleteFlag == false);
            if (existingBlog is null)
            {
                return NotFound("Blog not found.");
            }
            existingBlog.BlogTitle = blogTable.BlogTitle;
            existingBlog.BlogAuthor = blogTable.BlogAuthor;
            existingBlog.BlogContext = blogTable.BlogContext;
            _db.Entry(existingBlog).State = EntityState.Modified;
            var result = _db.SaveChanges();
            return Ok(result == 1 ? "Update Successful" : "Updated Failed");
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlogs(int id, BlogTable blogTable)
        {
            var item = _db.BlogTables.AsNoTracking().FirstOrDefault(x => x.BlogId == id && x.DeleteFlag == false);
            if (item is null)
            {
                return NotFound("Blog not found.");
            }

            if (!string.IsNullOrEmpty(blogTable.BlogTitle))
            {
                item.BlogTitle = blogTable.BlogTitle;
            }

            if (!string.IsNullOrEmpty(blogTable.BlogAuthor))
            {
                item.BlogAuthor = blogTable.BlogAuthor;
            }
            if (!string.IsNullOrEmpty(blogTable.BlogContext))
            {
                item.BlogContext = blogTable.BlogContext;
            }
            _db.Entry(item).State = EntityState.Modified;
            var result = _db.SaveChanges();
            return Ok(result == 1 ? "Patch Successful" : "Patch Failed");
        }

        [HttpDelete("${id}")]
        public IActionResult DeleteBlogs(int id)
        {
            var item = _db.BlogTables.AsNoTracking().FirstOrDefault(x => x.BlogId == id && x.DeleteFlag == false);
            if (item is null)
            {
                return NotFound("Blog not found.");
            }
            item.DeleteFlag = true;
            _db.Entry(item).State = EntityState.Deleted;
            var result = _db.SaveChanges();
            return Ok(result == 1 ? "Deletion Success" : "Deletion Failed");
        }
    }
}
