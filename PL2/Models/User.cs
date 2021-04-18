using System;
using System.Collections.Generic;
using System.Text;

namespace PL.Models
{
    public class User
    {
        public string Password { get; set; }
        public string Login { get; set; }
        public int Id { get; set; }
        public int WorkerId { get; set; }
    }
}
