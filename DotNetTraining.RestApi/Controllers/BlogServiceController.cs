using DotNetTraining.Domain.Services;
using DotNetTraining.RestApi.DataModel;
using DotNetTrainingDatabase.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNetTraining.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogServiceController : ControllerBase
    {
        private readonly BlogsServices _service;

        public BlogServiceController(BlogsServices service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetBlogs()
        {
            var list = _service.Blogs();
            return Ok(list);

        }

        [HttpGet]
        public IActionResult GetDeletedBlogs()
        {
            var list = _service.GetDeletedBlogs();

            return Ok(list);

        }
        [HttpGet("{id}")]
        public IActionResult GetBlogsId(int id)
        {
            var list = _service.GetBlogById(id);
            return Ok(list);
        }

        [HttpPost]
        public IActionResult CreateBlogs(BlogTable blogsDTO)
        {

            _service.CreateBlog(blogsDTO);
            return Ok(blogsDTO);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlogs(int id, BlogTable blogsDTO)
        {
            var item = _service.EditBlog(id, blogsDTO);
            return Ok();
        }
        [HttpPatch("{id}")]
        public IActionResult PatchBlogs(int id, BlogTable blogsDTO)
        {
            var items = _service.UpdateBlog(id, blogsDTO);
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBlogs(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var item = _service.DeleteBlog(id);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult ResotreBlogs(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var item = _service.RestoreBlog(id);
            return Ok();
        }
    }
}
