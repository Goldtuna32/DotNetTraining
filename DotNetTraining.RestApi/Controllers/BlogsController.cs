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
            var list = _db.BlogTables.AsNoTracking().ToList();
            return Ok(list);
        }
        [HttpGet("{id}")]
        public IActionResult GetBlogsId(int id)
        {
            var list = _db.BlogTables.AsNoTracking().FirstOrDefault(x => x.BlogId == id && x.DeleteFlag == false);
            if (list == null)
            {
                return NotFound();
            }
            return Ok(list);
        }

        [HttpPost]
        public IActionResult CreateBlogs(BlogTable blogTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _db.BlogTables.Add(blogTable);
            _db.SaveChanges();
            return Ok(blogTable);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBlogs(int id, BlogTable blogTable)
        {
            var item = _db.BlogTables.AsNoTracking().FirstOrDefault(x => x.BlogId == id && x.DeleteFlag == false);
            if (item == null)
            {
                return NotFound();
            }
            item.BlogTitle = blogTable.BlogTitle;
            if (blogTable.BlogTitle is null)
            {
                blogTable.BlogTitle = item.BlogTitle;
            }
            item.BlogAuthor = blogTable.BlogAuthor;
            if (blogTable.BlogAuthor is null)
            {
                blogTable.BlogAuthor = item.BlogAuthor;
            }

            item.BlogContext = blogTable.BlogContext;
            if (blogTable.BlogContext is null)
            {
                blogTable.BlogContext = item.BlogContext;
            }
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return Ok();
        }
        [HttpPatch]
        public IActionResult PatchBlogs()
        {
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBlogs(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var item = _db.BlogTables.AsNoTracking().FirstOrDefault(x => x.BlogId == id && x.DeleteFlag == false);
            item.DeleteFlag = true;
            _db.Entry(item).State = EntityState.Deleted;
            _db.SaveChanges();
            return Ok();
        }
    }
}
