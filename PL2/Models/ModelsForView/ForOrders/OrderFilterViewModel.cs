using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace PL.Models.ModelsForView
{
    public class OrderFilterViewModel
    {
        public OrderFilterViewModel(List<Client> clients, int? order,
            int? client)
        {
            var serviceList = clients.Distinct().ToList();

            serviceList.Insert(0, new Client() { Id = 0, Title = "All" });

            Clients = new SelectList(serviceList, "Id", "Title", client);

            SelectedOrder = order;
            SelectedClient = client;
        }
        public SelectList Clients { get; private set; }
        
        public int? SelectedClient { get; set; }
        public int? SelectedOrder { get; set; }
    }
}
