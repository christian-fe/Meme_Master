using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Memes.Models.Dto
{
    public class UserAuthLoginDto
    {
        [Required(ErrorMessage = "Mandatory Field")]
        public string User { get; set; }
        [Required(ErrorMessage = "Mandatory Field")]
        public string Password { get; set; }
    }
}

