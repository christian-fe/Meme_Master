using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Memes.Models
{
    public class Photo
    {
        [Key]
        public int PhotoId{ get; set; }
        public string PhotoName { get; set; }
        public string ImagePath { get; set; }
        public string TopText { get; set; }
        public string BottomText { get; set; }
        public enum MemeType { Animals, Movies, Cartoon, People }
        public MemeType Option { get; set; }
        public DateTime CreationDate { get; set; }
        //--------------------------------------------------------
        public int IdCategory { get; set; }
        [ForeignKey("IdCategory")]
        public Category Category { get; set; }
    }
}
