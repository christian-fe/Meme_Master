using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Memes.Models
{
    public class Meme
    {
        [Key]
        public int MemeId{ get; set; }
        public string MemeName { get; set; }
        public string ImagePath { get; set; }
        public string TopText { get; set; }
        public string BottomText { get; set; }
        public enum MemeType { Animals, Movies, Cartoon, People }
        public MemeType Category {get;set;}
        public DateTime CreationDate { get; set; }
        //--------------------------------------------------------
        public int IdCategory { get; set; }
        [ForeignKey("IdCategory")]
        public Category category { get; set; }
    }
}
