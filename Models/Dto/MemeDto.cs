using Memes.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static Memes.Models.Meme;

namespace Meme.Models.Dto
{
    public class MemeDto
    {
        public int MemeId { get; set; }
        [Required(ErrorMessage="Mandatory Field")]
        public string MemeName { get; set; }
        [Required(ErrorMessage = "Mandatory Field")]
        public string ImagePath { get; set; }
        public string TopText { get; set; }
        public string BottomText { get; set; }
        public MemeType Category { get; set; }
        //--------------------------------------------------------
        public int IdCategory { get; set; }        
        public Category category { get; set; }
    }
}
