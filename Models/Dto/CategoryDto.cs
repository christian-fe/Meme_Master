using System;
using System.ComponentModel.DataAnnotations;

namespace Meme.Models.Dto
{
    public class CategoryDto 
    {
        
        public int IdCategory { get; set; }
        [Required(ErrorMessage ="Mandatory field")]
        public string CategoryName { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
