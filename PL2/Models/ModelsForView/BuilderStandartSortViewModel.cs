using PL.Infrastructure.Sorting;
namespace PL.Models.ModelsForView
{
    public class BuilderStandartSortViewModel
    {
         
        public BuildStandartSortState Curent { get; set; }

        public BuildStandartSortState ComponentSort { get; set; }
        public BuildStandartSortState ServiceSort { get; set; }
        public BuildStandartSortState PriceSort { get; set; }
        public bool Up { get; set; }

        public BuilderStandartSortViewModel(BuildStandartSortState sortState)
        {
            
            Up = true;

            if(sortState == BuildStandartSortState.PriceDes || sortState == BuildStandartSortState.ServiceTitleDes
                || sortState == BuildStandartSortState.ComponentTitleDes)
            {
                Up = false;
            }
            switch (sortState)
            {
                case BuildStandartSortState.ServiceTitleAsc:
                    Curent = ServiceSort = BuildStandartSortState.ServiceTitleAsc;
                    break;
                case BuildStandartSortState.PriceAsc:
                    Curent = PriceSort = BuildStandartSortState.PriceAsc;
                    break;
                case BuildStandartSortState.ComponentTitleDes:
                    Curent = ComponentSort = BuildStandartSortState.ComponentTitleDes;
                    break;

                case BuildStandartSortState.ServiceTitleDes:
                    Curent = ServiceSort = BuildStandartSortState.ServiceTitleDes;
                    break;
                case BuildStandartSortState.PriceDes:
                    Curent = PriceSort = BuildStandartSortState.PriceDes;
                    break;
                default:
                    Curent = ComponentSort = BuildStandartSortState.ComponentTitleAsc;
                    break;
            }
        }
    }
}
