using DotNetTraining.RestApi.DataModel;

namespace DotNetTraining.RestApi.Services
{
    public class BlogsServices
    {
        public BlogsDTO GetBlogs()
        {
            return new BlogsDTO
            {
                BlogId = 1,
                BlogTitle = "Introduction to .NET 7",
                BlogAuthor = "Jane Doe",
                BlogContext = "This blog post introduces the new features in .NET 7...",
                DeleteFlag = false
            };
        }
    }
}
