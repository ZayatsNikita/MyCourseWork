using System;
using System.Collections.Generic;
using System.Text;

namespace PL.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AccsesLevel { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Role role &&
                   Id == role.Id;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
