using System;
using System.Collections.Generic;
using System.Text;

namespace BL.dtoModels
{
    public class Role
    {
        public int Id { get; set; }
        public string Title{ get; set; }
        public string Description { get; set; }
        public int AccsesLevel { get; set; }
    }
}
