using Microsoft.AspNetCore.Mvc;
using PL.Infrastructure.Extensions;
using PL.Infrastructure.Services.Abstract;
using PL.Infrastructure.Enumerators;
using PL.Models;
using PL.Models.ModelsForView;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PL.Controllers
{
    public class OrderController : Controller
    {
        private int pageSize = 6;
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
            
            
            Order order = new Order() { ClientId = customerId, ManagerId = managerId, MasterId = masterId, StartDate = startDate };
            _orderServices.Create(order);
            var list = cart.OrderLine.Values.Select(x => new OrderInfo() {OrderNumber = order.Id, BuildStandart =x.BuildStandart, CountOfServicesRendered = x.Count });
            
            foreach (var item in list)
            {
                _orderInfoServise.Create(item);
            }
            cart.Clear();
            cart.SaveToCoockie(HttpContext);
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
            Cart cart = new Cart();
            cart.GetFromCoockie(HttpContext);
            if((cart?.OrderLine?.Count() ?? 0) ==0)
            {
                ModelState.AddModelError("", "The shopping cart is empty");
                return RedirectToAction("ShowCart", "Cart");
            }
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

        public IActionResult OrderInfo(int orderId)
        {
            Order order = _orderServices.Read(minId: orderId, maxId: orderId).FirstOrDefault();
            ViewBag.Info = _orderInfoServise.Read(minOrderNumber: orderId, maxOrderNumber: orderId);
            ViewBag.Client = _clientServices.Read(MinId: order.ClientId, MaxId: order.ClientId).FirstOrDefault();
            return View(order);
        }

        public IActionResult ChangeableOrderList(int masterId = -1, int page = 1, bool  int order = -1, int client = -1, OrderSortState sortState = OrderSortState.OrderIdAsc)
        {
            ViewBag.MId = masterId;
            IEnumerable<Order> orders = _orderServices.Read(minMasterId: masterId, maxMasterId: masterId);
            
            if (client >= 1)
            {
                orders = orders.Where(x => x.ClientId == client);
            }
            if (order >= 1)
            {
                orders = orders.Where(x => x.Id == order);
            }
         
            var res = orders.Select(x=>new OrderMin {Id= x.Id, StartDate  = x.StartDate, Client = _clientServices.Read(MinId: x.ClientId, MaxId: x.ClientId).FirstOrDefault()});

            switch (sortState)
            {
                case OrderSortState.ClientTitleAsc:
                    res = res.OrderBy(x => x.Client.Title);
                    break;
                case OrderSortState.OrderIdAsc:
                    res = res.OrderBy(x => x.Id);
                    break;
                case OrderSortState.StartDateAsc:
                    res = res.OrderBy(x => x.StartDate);
                    break;
                case OrderSortState.ClientTitleDes:
                    res = res.OrderByDescending(x => x.Client.Title);
                    break;
                case OrderSortState.OrderIdDes:
                    res = res.OrderByDescending(x => x.Id);
                    break;
                default:
                    res = res.OrderByDescending(x => x.StartDate);
                    break;
            };
            int count = res.Count();
            var Items = res.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            OrderListViewModel data = new OrderListViewModel()
            {
                Standarts = Items,
                PageViewModel = new PageViewModel(count, page, pageSize),
                SortViewModel = new OrderSortViewModel(sortState),
                FilterViewModel = new OrderFilterViewModel(Items.Select(y=>y.Client).ToList(), order, client)
            };
            return View(data);
        }

        public IActionResult ConfirmOrder(int orderId)
        {
            Order order = _orderServices.Read(minId: orderId, maxId: orderId).FirstOrDefault();
            order.CompletionDate = DateTime.Now.Date;
            _orderServices.Update(order, CompletionDate: order.CompletionDate);
            return RedirectToAction(nameof(OrderInfo), new { OrderId = orderId });
        }


    }
}
