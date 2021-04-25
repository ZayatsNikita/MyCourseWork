using System;
using System.Collections.Generic;
using System.Text;

namespace BL.DtoModels.Combined
{
    public class FullOrder
    {
        public List<FullOrderInfo> fullOrderInfos { get; set; }
        public Order Order {get;set;}
    }
}
