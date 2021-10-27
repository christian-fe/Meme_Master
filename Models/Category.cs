using System;
using System.ComponentModel.DataAnnotations;

namespace Memes.Models
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class Category
    {
        [Key]
        public int IdCategory { get; set; }
        public string CategoryName { get; set; }
        public DateTime CreationDate { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

}
