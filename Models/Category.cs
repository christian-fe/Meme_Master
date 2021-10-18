using System;
using System.ComponentModel.DataAnnotations;

namespace Memes.Models
{
    public class Category
    {
        [Key]
        public int IdCategory { get; set; }
        public string CategoryName { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
