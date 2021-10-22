using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using static Memes.Models.Photo;

namespace Memes.Models.Dto
{
    public class PhotoUpdateDto
    {
        public int PhotoId { get; set; }
        [Required(ErrorMessage = "Mandatory Field")]
        public string PhotoName { get; set; }
        [Required(ErrorMessage = "Mandatory Field")]
        public string ImagePath { get; set; }
        public string TopText { get; set; }
        public string BottomText { get; set; }
        public MemeType Option { get; set; }
        //--------------------------------------------------------
        public int IdCategory { get; set; }
    }
}
