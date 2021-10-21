using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static Memes.Models.Photo;

namespace Memes.Models.Dto
{
    public class PhotoDto
    {
        public int PhotoId { get; set; }
        [Required(ErrorMessage="Mandatory Field")]
        public string PhotoName { get; set; }
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
