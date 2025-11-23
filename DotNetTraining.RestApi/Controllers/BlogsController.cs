using DotNetTraining.RestApi.DataModel;
using DotNetTrainingDatabase.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNetTraining.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly AppDbContext _db = new AppDbContext();

        [HttpGet]
        public IActionResult GetBlogs()
        {
            var list = _db.BlogTables
                .AsNoTracking()
                .Where(x => x.DeleteFlag == false)
                .ToList();
            if (list is null || list.Count == 0)
            {
                return NotFound();
            }

            List<BlogsDTO> blogsList = new List<BlogsDTO>();
            foreach (var item in list)
            {
                BlogsDTO blogsDTO = new BlogsDTO();
                blogsDTO.BlogId = item.BlogId;
                blogsDTO.BlogTitle = item.BlogTitle;
                blogsDTO.BlogAuthor = item.BlogAuthor;
                blogsDTO.BlogContext = item.BlogContext;

                blogsList.Add(blogsDTO);

            }

            return Ok(blogsList);

        }

        [HttpGet]
        public IActionResult GetDeletedBlogs()
        {
            var list = _db.BlogTables
                .AsNoTracking()
                .Where(x => x.DeleteFlag == true)
                .ToList();
            if (list is null || list.Count == 0)
            {
                return NotFound();
            }

            List<BlogsDTO> blogsList = new List<BlogsDTO>();
            foreach (var item in list)
            {
                BlogsDTO blogsDTO = new BlogsDTO();
                blogsDTO.BlogId = item.BlogId;
                blogsDTO.BlogTitle = item.BlogTitle;
                blogsDTO.BlogAuthor = item.BlogAuthor;
                blogsDTO.BlogContext = item.BlogContext;

                blogsList.Add(blogsDTO);

            }

            return Ok(blogsList);

        }
        [HttpGet("{id}")]
        public IActionResult GetBlogsId(int id)
        {
            var list = _db.BlogTables.AsNoTracking().FirstOrDefault(x => x.BlogId == id && x.DeleteFlag == false);
            if (list == null)
            {
                return NotFound();
            }
            BlogsDTO blogsDTO = new BlogsDTO()
            {
                BlogId = list.BlogId,
                BlogTitle = list.BlogTitle,
                BlogAuthor = list.BlogAuthor,
                BlogContext = list.BlogContext
            };
            return Ok(blogsDTO);
        }

        [HttpPost]
        public IActionResult CreateBlogs(BlogsDTO blogsDTO)
        {

            if (blogsDTO.BlogTitle is null || blogsDTO.BlogAuthor is null || blogsDTO.BlogContext is null)
            {
                return BadRequest();
            }
            BlogTable blogTable = new BlogTable()
            {
                BlogTitle = blogsDTO.BlogTitle,
                BlogAuthor = blogsDTO.BlogAuthor,
                BlogContext = blogsDTO.BlogContext,
                DeleteFlag = false
            };
            _db.BlogTables.Add(blogTable);
            _db.SaveChanges();
            return Ok(blogTable);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlogs(int id, BlogsDTO blogsDTO)
        {
            var item = _db.BlogTables.AsNoTracking().FirstOrDefault(x => x.BlogId == id && x.DeleteFlag == false);
            if (item == null)
            {
                return NotFound();
            }
            item.BlogTitle = blogsDTO.BlogTitle;
            if (blogsDTO.BlogTitle is null)
            {
                blogsDTO.BlogTitle = item.BlogTitle;
            }
            item.BlogAuthor = blogsDTO.BlogAuthor;
            if (blogsDTO.BlogAuthor is null)
            {
                blogsDTO.BlogAuthor = item.BlogAuthor;
            }

            item.BlogContext = blogsDTO.BlogContext;
            if (blogsDTO.BlogContext is null)
            {
                blogsDTO.BlogContext = item.BlogContext;
            }
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return Ok();
        }
        [HttpPatch("{id}")]
        public IActionResult PatchBlogs(int id, BlogsDTO blogsDTO)
        {
            var items = _db.BlogTables.AsNoTracking().FirstOrDefault(x => x.BlogId == id && x.DeleteFlag == false);
            if (items == null)
            {
                return NotFound();
            }
            if (!string.IsNullOrEmpty(blogsDTO.BlogTitle))
            {
                items.BlogTitle = blogsDTO.BlogTitle;
            }
            if (!string.IsNullOrEmpty(blogsDTO.BlogAuthor))
            {
                items.BlogAuthor = blogsDTO.BlogAuthor;
            }
            if (!string.IsNullOrEmpty(blogsDTO.BlogContext))
            {
                items.BlogContext = blogsDTO.BlogContext;
            }
            _db.Entry(items).State = EntityState.Modified;
            var result = _db.SaveChanges();
            return Ok(result == 1 ? "Patching Successful" : "Patching Failed");
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBlogs(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var item = _db.BlogTables.AsNoTracking().FirstOrDefault(x => x.BlogId == id && x.DeleteFlag == false);
            BlogsDTO blogsDTO = new BlogsDTO();
            blogsDTO.DeleteFlag = true; 
            item.DeleteFlag = blogsDTO.DeleteFlag;
            _db.Entry(item).State = EntityState.Deleted;
            _db.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult ResotreBlogs(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var item = _db.BlogTables.AsNoTracking().FirstOrDefault(x => x.BlogId == id && x.DeleteFlag == true);
            BlogsDTO blogsDTO = new BlogsDTO();
            blogsDTO.DeleteFlag = false; 
            item.DeleteFlag = blogsDTO.DeleteFlag;
            _db.Entry(item).State = EntityState.Deleted;
            _db.SaveChanges();
            return Ok();
        }
    }
}
