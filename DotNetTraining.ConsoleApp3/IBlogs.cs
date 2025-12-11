using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTraining.ConsoleApp3
{
    public interface IBlogs
    {
        [Get("/api/blogs")]
        Task<List<BlogTable>> Blogs();

        [Get("/api/blogs/{id}")]
        Task <BlogTable> GetBlogById(int id);
    }

    public class BlogTable
    {
        public int BlogId { get; set; }

        public string BlogTitle { get; set; } = null!;

        public string BlogAuthor { get; set; } = null!;

        public string BlogContext { get; set; } = null!;

        public bool? DeleteFlag { get; set; }
    }
}
