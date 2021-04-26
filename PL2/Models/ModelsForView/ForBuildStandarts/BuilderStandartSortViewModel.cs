using PL.Infrastructure.Sorting;
namespace PL.Models.ModelsForView
{
    public class BuilderStandartSortViewModel
    {
         
        public BuildStandartSortState Current { get; set; } 
        public BuildStandartSortState ComponentSort { get; set; }
        public BuildStandartSortState ServiceSort { get; set; }
        public BuildStandartSortState PriceSort { get; set; }

        public BuilderStandartSortViewModel(BuildStandartSortState sortState)
        {

            PriceSort = sortState == BuildStandartSortState.PriceAsc ? BuildStandartSortState.PriceDes : BuildStandartSortState.PriceAsc;
            
            ComponentSort = sortState == BuildStandartSortState.ComponentTitleAsc ? BuildStandartSortState.ComponentTitleDes : BuildStandartSortState.ComponentTitleAsc;
            
            ServiceSort = sortState == BuildStandartSortState.ServiceTitleAsc ? BuildStandartSortState.ServiceTitleDes : BuildStandartSortState.ServiceTitleAsc;

            Current = sortState;
        }
    }
}
