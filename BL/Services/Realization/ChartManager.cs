using BL.DtoModels;
using BL.Services.Abstract;
using DL.Entities;
using DL.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BL.Services
{
    public class ChartManager : IChartManager
    {
        private IOrderEntityRepository _ordersRepository;
        
        private IOrderInfoServices _orderInfoServices;
        
        private IWorkerEntityRepo _workerRepository;

        public ChartManager(IOrderEntityRepository ordersRepository, IOrderInfoServices orderInfoServices, IWorkerEntityRepo workerRepository)
        {
            _ordersRepository = ordersRepository;
            
            _orderInfoServices = orderInfoServices;

            _workerRepository = workerRepository;
        }

        public List<OrderInfo> GetInformationAboutTheServicesOrdered(DateTime? from, DateTime? to)
        {
            var orders = _ordersRepository.ReadOutstandingOrders().Select(x => new Order
            {
                Id = x.Id,
                ManagerId = x.ManagerId,
                MasterId = x.MasterId,
                ClientId = x.ClientId,
                StartDate = x.StartDate,
                CompletionDate = x.CompletionDate,
            });

            if (from != null)
            {
                orders = orders.Where(x => x.StartDate >= from);
            }

            if (to != null)
            {
                orders = orders.Where(x => x.StartDate <= to);
            }

            List<OrderInfo> result = new List<OrderInfo>();
            
            foreach (var order in orders)
            {
                var currentData = _orderInfoServices.Read().Where(x => x.OrderNumber == order.Id);
                
                result.AddRange(currentData);
            }

            return result;
        }

        public Dictionary<Worker, decimal> GetInformationAboutProfitByMasters(DateTime? from, DateTime? to)
        {
            IEnumerable<OrderEntity> orders = _ordersRepository.ReadComplitedOrders();

            if (from != null)
            {
                orders = orders.Where(x => x.StartDate >= from);
            }
            
            if (to != null)
            {
                orders = orders.Where(x => x.StartDate <= to);
            }

            var grouped = orders.GroupBy(x => x.MasterId);

            List<Worker> workers = new List<Worker>();

            foreach (var item in grouped)
            {
                var worker = _workerRepository.ReadById(item.Key);

                if (worker != null)
                {
                    workers.Add(new Worker
                    {
                        PassportNumber = worker.PassportNumber,
                        PersonalData = worker.PersonalData,
                    });
                }
            }
            Dictionary<Worker, decimal> result = new Dictionary<Worker, decimal>();
            
            grouped.ToList().ForEach(x => result.Add(workers.Find(y => y.PassportNumber == x.Key), x.Sum(z => GetProfitFromOneOrder(z))));
            
            return result;
        }

        public Dictionary<Worker, decimal> GetInformationAboutProfitByManagers(DateTime? from, DateTime? to)
        {
            IEnumerable<OrderEntity> orders = _ordersRepository.ReadComplitedOrders();

            if (from != null)
            {
                orders = orders.Where(x => x.StartDate >= from);
            }
            if (to != null)
            {
                orders = orders.Where(x => x.StartDate <= to);
            }

            var grouped = orders.GroupBy(x => x.ManagerId);

            List<Worker> workers = new List<Worker>();

            foreach (var item in grouped)
            {
                var worker = _workerRepository.ReadById(item.Key);

                if (worker != null)
                {
                    workers.Add(new Worker 
                    {
                        PassportNumber = worker.PassportNumber,
                        PersonalData = worker.PersonalData,
                    });
                }
            }

            Dictionary<Worker, decimal> result = new Dictionary<Worker, decimal>();
            
            grouped.ToList().ForEach(x => result.Add(workers.Find(y => y.PassportNumber == x.Key), x.Sum(z => GetProfitFromOneOrder(z))));
            
            return result;
        }

        private decimal GetProfitFromOneOrder(OrderEntity order)
        {
            OrderInfo[] orderInfos = _orderInfoServices.Read().Where(x => x.OrderNumber == order.Id).ToArray();

            decimal sum = 0;

            for (int i = 0; i < orderInfos.Length; i++)
            {
                sum += orderInfos[i].BuildStandart.ComonPrice * orderInfos[i].CountOfServicesRendered;
            }
            return sum;
        }
    }
}
