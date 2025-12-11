using DotNetTraining.RestApi.DataModel;
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
        private readonly AppDbContext _db;

        public BlogsControllerExample(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetBlogs()
        {
            var list = _db.BlogTables
                .AsNoTracking()
                .Where(x => x.DeleteFlag == false)
                .ToList();
            if (list.Count == 0)
            {
                return NotFound("No blogs found.");
            }
            List<BlogsDTO> blogsList = new List<BlogsDTO>();
            foreach (var blog in list)
            {
                blogsList.Add(new BlogsDTO
                {
                    BlogId = blog.BlogId,
                    BlogTitle = blog.BlogTitle,
                    BlogAuthor = blog.BlogAuthor,
                    BlogContext = blog.BlogContext,
                    DeleteFlag = blog.DeleteFlag
                });
            }
            return Ok(blogsList);
        }

        [HttpPost]
        public IActionResult CreateBlogs(BlogsDTO blogsDTO)
        {
            if (blogsDTO is null)
            {
                return BadRequest("Invalid blog data.");
            }
            BlogTable blogsTable = new BlogTable
            {
                BlogTitle = blogsDTO.BlogTitle,
                BlogAuthor = blogsDTO.BlogAuthor,
                BlogContext = blogsDTO.BlogContext,
                DeleteFlag = false
            };
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
            BlogsDTO blogDTO = new BlogsDTO
            {
                BlogId = item.BlogId,
                BlogTitle = item.BlogTitle,
                BlogAuthor = item.BlogAuthor,
                BlogContext = item.BlogContext,
                DeleteFlag = item.DeleteFlag
            };
            return Ok(item);
        }
        [HttpPut("${id}")]
        public IActionResult UpdateBlogs(int id, BlogsDTO blogsDTO)
        {
            var existingBlog = _db.BlogTables.AsNoTracking().FirstOrDefault(x => x.BlogId == id && x.DeleteFlag == false);
            if (existingBlog is null)
            {
                return NotFound("Blog not found.");
            }
            existingBlog.BlogTitle = blogsDTO.BlogTitle;
            existingBlog.BlogAuthor = blogsDTO.BlogAuthor;
            existingBlog.BlogContext = blogsDTO.BlogContext;
            _db.Entry(existingBlog).State = EntityState.Modified;
            var result = _db.SaveChanges();
            return Ok(result == 1 ? "Update Successful" : "Updated Failed");
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlogs(int id, BlogsDTO blogsDTO)
        {
            var item = _db.BlogTables.AsNoTracking().FirstOrDefault(x => x.BlogId == id && x.DeleteFlag == false);
            if (item is null)
            {
                return NotFound("Blog not found.");
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
            BlogsDTO blogDTO = new BlogsDTO();
            blogDTO.DeleteFlag = true; 
            item.DeleteFlag = blogDTO.DeleteFlag;
            _db.Entry(item).State = EntityState.Modified;
            var result = _db.SaveChanges();
            return Ok(result == 1 ? "Deletion Success" : "Deletion Failed");
        }

        [HttpDelete("${id}")]
        public IActionResult RestoreBlogs(int id)
        {
            var item = _db.BlogTables.AsNoTracking().FirstOrDefault(x => x.BlogId == id && x.DeleteFlag == true);
            if (item is null)
            {
                return NotFound("Blog not found.");
            }
            BlogsDTO blogDTO = new BlogsDTO();
            blogDTO.DeleteFlag = false; 
            item.DeleteFlag = blogDTO.DeleteFlag;
            _db.Entry(item).State = EntityState.Modified;
            var result = _db.SaveChanges();
            return Ok(result == 1 ? "Restoration Success" : "Restoration Failed");
        }
    }
}
