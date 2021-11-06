using System;
using System.Collections.Generic;
using System.Text;

namespace BL.DtoModels.Combined
{
    public class FullOrder
    {
        public List<OrderInfo> orderInfos { get; set; }

        public Order Order {get;set;}
    }
}
