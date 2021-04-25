namespace BL.DtoModels
{
    public class OrderInfo
    {
        public int OrderNumber { get; set; }
        public int CountOfServicesRendered { get; set; }
        public Combined.BuildStandart BuildStandart { get; set; }
        public int Id { get; set; }
    }
}
