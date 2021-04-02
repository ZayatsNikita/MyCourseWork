using System;
using System.Collections.Generic;
using System.Text;

namespace BL.dtoModels
{
    public class User
    {
        public string Password { get; set; }
        public string Login { get; set; }
        public int Id { get; set; }
        public int WorkerId { get; set; }
    }
}
