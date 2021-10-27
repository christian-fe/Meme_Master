using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Memes.Models.Dto
{
    public class UserAuthDto
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Mandatory Field")]
        public string User { get; set; }
        [Required(ErrorMessage = "Mandatory Field")]
        [StringLength(10,MinimumLength =4,ErrorMessage ="The password needs to be between 4 to 10 characters")]
        public string Password { get; set; }
    }
}