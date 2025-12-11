using DotNetTrainingDatabase.Models;

namespace DotNetTraining.Domain.Services
{
    public interface IBlogsServices
    {
        List<BlogTable> Blogs();
        void CreateBlog(BlogTable blog);
        bool DeleteBlog(int id);
        BlogTable EditBlog(int id, BlogTable blogsDTO);
        BlogTable GetBlogById(int id);
        List<BlogTable> GetDeletedBlogs();
        bool RestoreBlog(int id);
        BlogTable UpdateBlog(int id, BlogTable blogsDTO);
    }
}