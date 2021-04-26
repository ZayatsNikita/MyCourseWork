using Microsoft.AspNetCore.Mvc;
using PL.Infrastructure.Extensions;
using PL.Infrastructure.Services.Abstract;
using PL.Models;
using PL.Models.ModelsForView;
using System.Collections.Generic;
using System.Linq;
using System;
namespace PL.Controllers
{
    public class OrderController : Controller
    {
        private IWorkerServices _workerServises;
        private IFullUserServices _fullUserServices;
        private IClientServices _clientServices;
        private IOrderServices _orderServices;
        IOrderInfoServise _orderInfoServise;
        public OrderController(IFullUserServices fullUserServices, IWorkerServices workerServices, IClientServices clientServices, IOrderServices orderServices, IOrderInfoServise orderInfoServise) 
        {
            _fullUserServices = fullUserServices;
            _workerServises = workerServices;
            _clientServices = clientServices;
            _orderServices = orderServices;
            _orderInfoServise = orderInfoServise;
        }

        [HttpPost]
        public ActionResult SaveOrder(int masterId, int managerId, int customerId, DateTime startDate)
        {
            Cart cart = new Cart();
            cart.GetFromCoockie(HttpContext);
            
            Order order = new Order() { ClientId = customerId, ManagerId = managerId, MasterId = managerId, StartDate = startDate };
            _orderServices.Create(order);
            var list = cart.OrderLine.Values.Select(x => new OrderInfo() {OrderNumber = order.Id, BuildStandart =x.BuildStandart, CountOfServicesRendered = x.Count });

            foreach (var item in list)
            {
                _orderInfoServise.Create(item);
            }
            return View();
        }

        public ActionResult CancelOrder()
        {
            Cart cart = new Cart();
            cart.GetFromCoockie(HttpContext);
            cart.Clear();
            cart.SaveToCoockie(HttpContext);
            return RedirectToAction("StartMenu", "Manager");
        }

        public IActionResult MakingAnOrder()
        {
            List<Worker> workers =  _workerServises.Read();
            List<FullUser> users = new List<FullUser>();
            foreach (var item in workers)
            {
                users.Add(_fullUserServices.Read(item.PassportNumber));
            }
            
            ViewData["Master"] = users.Where(x=>x.Roles.Count(y=>y.Title.Equals("Master"))!=0);
            ViewData["Manager"] = users.Where(x=>x.Roles.Count(y=>y.Title.Equals("Manager"))!=0);
            ViewData["Client"] = _clientServices.Read();

            return View();
        }
    }
}
