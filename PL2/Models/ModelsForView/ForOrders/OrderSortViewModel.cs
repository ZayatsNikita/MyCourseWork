using PL.Infrastructure.Sorting;
namespace PL.Models.ModelsForView
{
    public class OrderSortViewModel
    {
         
        public OrderSortState Current { get; set; } 
        public OrderSortState ClientSort { get; set; }
        public OrderSortState IdSort { get; set; }
        public OrderSortState StartDateSort { get; set; }

        public OrderSortViewModel(OrderSortState sortState)
        {

            StartDateSort = sortState == OrderSortState.StartDateAsc ? OrderSortState.StartDateDes : OrderSortState.StartDateAsc;
            
            ClientSort = sortState == OrderSortState.ClientTitleAsc ? OrderSortState.ClientTitleDes : OrderSortState.ClientTitleAsc;
            
            IdSort = sortState == OrderSortState.OrderIdAsc ? OrderSortState.OrderIdDes : OrderSortState.OrderIdAsc;

            Current = sortState;
        }
    }
}
