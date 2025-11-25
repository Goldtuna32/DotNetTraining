namespace DotNetTraining.MinimalApi.Endpoints.Blogs;

public static class BlogEndpoint
{
    public static void MapBlogEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/blogs", () =>
        {
            AppDbContext db = new AppDbContext();
            var model = db.BlogTables.AsNoTracking().ToList();
            return Results.Ok(model);
        })
.WithName("GetBlogs")
.WithOpenApi();

        app.MapPost("/blogs", (BlogTable blog) =>
        {
            AppDbContext db = new AppDbContext();
            db.BlogTables.Add(blog);
            db.SaveChanges();
            return Results.Ok(blog);
        })
        .WithName("CreateBlgos")
        .WithOpenApi();

        app.MapGet("/blogs/{id}", (int id) =>
        {
            AppDbContext db = new AppDbContext();
            var item = db.BlogTables
            .AsNoTracking()
            .FirstOrDefault(x => x.BlogId == id && x.DeleteFlag == false);
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
            AppDbContext db = new AppDbContext();
            var item = db.BlogTables
            .AsNoTracking()
            .FirstOrDefault(x => x.BlogId == id && x.DeleteFlag == false);
            if (item is null)
            {
                return Results.NotFound();
            }
            item.BlogTitle = blogsDTO.BlogTitle;
            item.BlogAuthor = blogsDTO.BlogAuthor;
            item.BlogContext = blogsDTO.BlogContext;
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
            return Results.Ok(item);
        })
        .WithName("EditBlogs")
        .WithOpenApi();

        app.MapPatch("/blogs/{id}", (int id, BlogTable blogsDTO) =>
        {
            AppDbContext db = new AppDbContext();
            var item = db.BlogTables
            .AsNoTracking()
            .FirstOrDefault(x => x.BlogId == id && x.DeleteFlag == false);
            if (item is null)
            {
                return Results.NotFound();
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
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
            return Results.Ok(item);
        })
        .WithName("UpdateBlogs")
        .WithOpenApi();

        app.MapDelete("/blogs/{id}", (int id) =>
        {
            AppDbContext db = new AppDbContext();
            var item = db.BlogTables
            .AsNoTracking()
            .FirstOrDefault(x => x.BlogId == id && x.DeleteFlag == false);
            if (item is null)
            {
                return Results.NotFound();
            }
            db.Entry(item).State = EntityState.Deleted;
            db.SaveChanges();
            return Results.Ok();
        })
        .WithName("DeleteBlogs")
        .WithOpenApi();

    }
}
