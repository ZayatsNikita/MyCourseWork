﻿using Microsoft.AspNetCore.Mvc;
using PL.Infrastructure.Extensions;
using PL.Infrastructure.Services.Abstract;
using PL.Models;
using PL.Models.ModelsForView;
using System.Collections.Generic;
using System.Linq;
using System;
using PL.Infrastructure.Sorting;

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

        public IActionResult ChangeableOrderList(int masterId, int page = 1, int orderNumber = 0, int client = 0, OrderSortState sortState = OrderSortState.OrderIdAsc)
        {
            IEnumerable<Order> orders = _orderServices.Read(minMasterId: masterId, maxMasterId: masterId);

            if (client < 1)
            {
                orders = orders.Where(x => x.ClientId == client);
            }
            if (orderNumber < 1)
            {
                orders = orders.Where(x => x.Id == orderNumber);
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
                FilterViewModel = new OrderFilterViewModel(Items.Select(y=>y.Client).ToList(), orderNumber, client)
            };


            return View();
        }




    }
}
