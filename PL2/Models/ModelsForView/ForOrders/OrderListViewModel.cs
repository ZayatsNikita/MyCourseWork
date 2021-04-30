using System.Collections.Generic;

namespace PL.Models.ModelsForView
{
    public class OrderListViewModel
    {
        public IEnumerable<OrderMin> Standarts { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public OrderFilterViewModel FilterViewModel { get; set; }
        public OrderSortViewModel SortViewModel { get; set; }
    }
}
