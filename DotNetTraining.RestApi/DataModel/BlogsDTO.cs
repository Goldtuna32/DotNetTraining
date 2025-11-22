namespace DotNetTraining.RestApi.DataModel
{
    public class BlogsDTO
    {
        public int BlogId { get; set; }
        public string? BlogTitle { get; set; }
        public string? BlogAuthor { get; set; }
        public string? BlogContext { get; set; }
        public bool DeleteFlag { get; set; }
    }
}
