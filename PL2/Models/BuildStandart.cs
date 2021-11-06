using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PL.Models
{
    public class BuildStandart
    {
        public int Id { get; set; }
        
        public Service Service { get; set; }

        public Componet Componet { get; set; }
    }
}
