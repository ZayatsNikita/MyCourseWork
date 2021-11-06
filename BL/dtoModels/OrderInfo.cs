using BL.DtoModels.Combined;

namespace BL.DtoModels
{
    public class OrderInfo
    {
        public int OrderNumber { get; set; }
        public int CountOfServicesRendered { get; set; }
        public FullServiceComponents BuildStandart { get; set; }
        public int Id { get; set; }
    }
}
