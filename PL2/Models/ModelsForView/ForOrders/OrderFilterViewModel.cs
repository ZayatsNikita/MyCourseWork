using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using PL.Infrastructure.Enumerators;
namespace PL.Models.ModelsForView
{
    public class OrderFilterViewModel
    {
        public OrderFilterViewModel(List<Client> clients, int? order,
            int? client, OrderStatus status)
        {
            var serviceList = clients.Distinct().ToList();

            serviceList.Insert(0, new Client() { Id = 0, Title = "All" });

            Clients = new SelectList(serviceList, "Id", "Title", client);
            


            SelectedOrder = order;
            SelectedClient = client;
            SelectedStatus = status;
        }
        public SelectList Clients { get; private set; }
        public SelectList OrderStates { get; private set; }
        public OrderStatus SelectedStatus { get; set; }
        public int? SelectedClient { get; set; }
        public int? SelectedOrder { get; set; }
    }
}
