using DotNetTraining.Domain.Services;

namespace DotNetTraining.MinimalApi.Endpoints.Blogs;

public static class BlogServiceEndpoint
{
    private readonly static BlogsServices _service = new BlogsServices();

    public static void MapBlogEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/blogs", () =>
        {
            var model = _service.Blogs();
            return Results.Ok(model);
        })
.WithName("GetBlogs")
.WithOpenApi();

        app.MapPost("/blogs", (BlogTable blog) =>
        {
            _service.CreateBlog(blog);
            return Results.Ok(blog);
        })
        .WithName("CreateBlgos")
        .WithOpenApi();

        app.MapGet("/blogs/{id}", (int id) =>
        {
            var item = _service.GetBlogById(id);
            if (item is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(item);
        })
        .WithName("GetBlogsByID")
        .WithOpenApi();

        app.MapPut("/blogs/{id}", (int id, BlogTable blogsDTO) =>
        {
             
            var item = _service.EditBlog(id, blogsDTO);
            return Results.Ok(item);
        })
        .WithName("EditBlogs")
        .WithOpenApi();

        app.MapPatch("/blogs/{id}", (int id, BlogTable blogsDTO) =>
        {
           
            var item = _service.UpdateBlog(id, blogsDTO);
            return Results.Ok(item);
        })
        .WithName("UpdateBlogs")
        .WithOpenApi();

        app.MapDelete("/blogs/{id}", (int id) =>
        {
             
            var item = _service.DeleteBlog(id);
            return Results.Ok();
        })
        .WithName("DeleteBlogs")
        .WithOpenApi();

    }
}
