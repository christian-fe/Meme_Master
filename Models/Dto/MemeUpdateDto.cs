using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using static Memes.Models.Meme;

namespace Meme.Models.Dto
{
    public class MemeUpdateDto
    {
        public int MemeId { get; set; }
        [Required(ErrorMessage = "Mandatory Field")]
        public string MemeName { get; set; }
        [Required(ErrorMessage = "Mandatory Field")]
        public string ImagePath { get; set; }
        public string TopText { get; set; }
        public string BottomText { get; set; }
        public MemeType Category { get; set; }
        //--------------------------------------------------------
        public int IdCategory { get; set; }
    }
}
