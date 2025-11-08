using System;
using System.Collections.Generic;

namespace DotNetTrainingDatabase.Models;

public partial class BlogTable
{
    public int BlogId { get; set; }

    public string BlogTitle { get; set; } = null!;

    public string BlogAuthor { get; set; } = null!;

    public string BlogContext { get; set; } = null!;

    public bool? DeleteFlag { get; set; }
}
