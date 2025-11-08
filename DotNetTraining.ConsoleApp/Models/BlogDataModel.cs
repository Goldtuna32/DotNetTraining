using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTraining.ConsoleApp.Models
{
    public class BlogDapperDataModel
    {
        public int Blog_id { get; set; }

        public string Blog_title { get; set; }

        public string Blog_author { get; set; }

        public string Blog_context { get; set; }

        public int Delete_flag { get; set; }
    }

    [Table("blogTable")]
    public class BlogDataModel
    {
        [Key]
        [Column("Blog_id")]
        public int Blog_id { get; set; }

        [Column("Blog_title")]
        public string Blog_title { get; set; }

        [Column("Blog_author")]
        public string Blog_author { get; set; }

        [Column("Blog_context")]
        public string Blog_context { get; set; }

        [Column("Delete_flag")]
        public bool Delete_flag { get; set; }
    }
}
