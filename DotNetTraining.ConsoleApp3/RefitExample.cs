using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTraining.ConsoleApp3
{
    public class RefitExample
    {
        public async Task Run()
        {
            var blogApi = RestService.For<IBlogs>("https://localhost:7080");
            var blogs = await blogApi.Blogs();
            foreach (var blog in blogs)
            {
                Console.WriteLine($"{blog.BlogId} - {blog.BlogTitle}");
            }

            
            //try
            //{
            //    var blogItem = await blogApi.GetBlogById(1);
            //}
            //catch (ApiException apiEx)
            //{
            //    Console.WriteLine($"API Error: {apiEx.StatusCode} - {apiEx.Content}");

            //}


        }
    }
}
