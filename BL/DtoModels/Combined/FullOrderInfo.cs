using System;
using System.Collections.Generic;
using System.Text;

namespace BL.DtoModels.Combined
{
    public class FullOrderInfo
    {
        public int Id { get; set; }
        public BuildStandart BuildStandart { get; set; }
        public int CountOfServiceOrdered { get; set; }
        public int OrderId { get; set; }
    }
}
