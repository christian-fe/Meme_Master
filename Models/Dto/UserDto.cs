using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memes.Models.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public byte[] PasswordHash { get; set; }
    }
}
