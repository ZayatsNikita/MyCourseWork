using System;
using System.Collections.Generic;
using System.Text;

namespace BL.DtoModels
{
    public class OrderInfo
    {
        public int OrderNumber { get; set; }
        public int CountOfServicesRendered { get; set; }
        public int ServiceId { get; set; }
        public int Id { get; set; }
    }
}
