using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Memes.Models.Photo;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Memes.Models.Dto
{
    public class PhotoCreateDto
    {
        [Required(ErrorMessage = "Mandatory Field")]
        public string PhotoName { get; set; }
        [Required(ErrorMessage = "Mandatory Field")]
        public string ImagePath { get; set; }
        public IFormFile Photo { get; set; }
        public string TopText { get; set; }
        public string BottomText { get; set; }
        public MemeType Category { get; set; }
        //--------------------------------------------------------
        public int IdCategory { get; set; }
    }
}
