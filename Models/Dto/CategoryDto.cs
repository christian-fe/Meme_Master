using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
