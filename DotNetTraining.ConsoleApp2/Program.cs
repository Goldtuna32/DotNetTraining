// See https://aka.ms/new-console-template for more information
using DotNetTrainingDatabase.Models;

Console.WriteLine("Hello, World!");

AppDbContext db = new AppDbContext();
var list = db.BlogTables.ToList();
foreach(var item in list)
{
    Console.WriteLine(item.BlogId);
    Console.WriteLine(item.BlogTitle);
    Console.WriteLine(item.BlogAuthor);
    Console.WriteLine(item.BlogContext);
}
