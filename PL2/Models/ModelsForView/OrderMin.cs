using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PL.Models.ModelsForView
{
    public class OrderMin
    {
        public int Id { get; set; }
        public DateTime? StartDate { get; set; }
        public Client Client { get; set; }
    }
}
