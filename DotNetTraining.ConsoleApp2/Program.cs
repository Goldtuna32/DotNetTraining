// See https://aka.ms/new-console-template for more information
using DotNetTrainingDatabase.Models;
using Newtonsoft.Json;

//Console.WriteLine("Hello, World!");

//AppDbContext db = new AppDbContext();
//var list = db.BlogTables.ToList();
//foreach(var item in list)
//{
//    Console.WriteLine(item.BlogId);
//    Console.WriteLine(item.BlogTitle);
//    Console.WriteLine(item.BlogAuthor);
//    Console.WriteLine(item.BlogContext);
//}

//var blog = new BlogTable
//{
//    Id = 1,
//    Title = "test title",
//    Author = "test author",
//    Content = "test content"
//};
//string str = blog.ToJson();
//Console.WriteLine(str);
string str = "{\"Id\":1,\"Title\":\"test title\",\"Author\":\"test author\",\"Content\":\"test content\"}";
var blog2 = JsonConvert.DeserializeObject<BlogTable>(str);
Console.WriteLine(blog2.Title);
Console.ReadLine();

public class BlogTable
{
    public int Id { get; set; }

    public string Title { get; set; }
    public string Author { get; set; }
    public string Content { get; set; }
}

public static class Extension
{
    public static string ToJson(this object obj)
    {
        string str = JsonConvert.SerializeObject(obj, Formatting.Indented);
        return str;
    }
}
