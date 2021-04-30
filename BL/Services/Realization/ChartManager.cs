using System;
using System.Collections.Generic;
using System.Text;
using BL.DtoModels;
using System.Linq;
using BL.DtoModels.Combined;
using BL.Services.Abstract;
namespace BL.Services
{
    public class ChartManager : IChartManager
    {
        private IOrderServices _orderServices;
        private IOrderInfoServices _orderInfoServices;
        private IWorkerServices _workerServices;

        public ChartManager(IOrderServices orderServices, IOrderInfoServices orderInfoServices, IWorkerServices workerServices)
        {
            _orderServices = orderServices;
            _orderInfoServices = orderInfoServices;
            _workerServices = workerServices;
        }

        public List<OrderInfo> GetInformationAboutTheServicesOrdered(DateTime? from, DateTime? to)
        {
            List<Order> orders = _orderServices.ReadOutstandingOrders(from, to);
            
            List<OrderInfo> result = new List<OrderInfo>();
            
            foreach (var item in orders)
            {
                var currentData = _orderInfoServices.Read(minOrderNumber: item.Id, maxOrderNumber: item.Id);
                result.AddRange(currentData);
            }
            return result;
        }

        public Dictionary<Worker, decimal> GetInformationAboutProfitByMasters(DateTime? from, DateTime? to)
        {
            List<Order> orders = _orderServices.ReadComplitedOrders(from, to);

            var grouped = orders.GroupBy(x => x.MasterId);

            List<Worker> workers = new List<Worker>();

            foreach (var item in grouped)
            {
                Worker worker = _workerServices.Read(minPassportNumber: item.Key, maxPassportNumber: item.Key).FirstOrDefault();
                if (worker != null)
                {
                    workers.Add(worker);
                }
            }
            Dictionary<Worker, decimal> result = new Dictionary<Worker, decimal>();
            grouped.ToList().ForEach(x => result.Add(workers.Find(y => y.PassportNumber == x.Key), x.Sum(z => GetProfitFromOneOrder(z))));
            return result;
        }
        public Dictionary<Worker, decimal> GetInformationAboutProfitByManagers(DateTime? from, DateTime? to)
        {
            List<Order> orders = _orderServices.ReadComplitedOrders(from, to);

            var grouped = orders.GroupBy(x => x.ManagerId);

            List<Worker> workers = new List<Worker>();

            foreach (var item in grouped)
            {
                Worker worker = _workerServices.Read(minPassportNumber: item.Key, maxPassportNumber: item.Key).FirstOrDefault();
                if (worker != null)
                {
                    workers.Add(worker);
                }
            }
            Dictionary<Worker, decimal> result = new Dictionary<Worker, decimal>();
            grouped.ToList().ForEach(x => result.Add(workers.Find(y => y.PassportNumber == x.Key), x.Sum(z => GetProfitFromOneOrder(z))));
            return result;
        }

        private decimal GetProfitFromOneOrder(Order order)
        {
            OrderInfo[] orderInfos = _orderInfoServices.Read(minOrderNumber: order.Id, maxOrderNumber: order.Id).ToArray();
            decimal sum = 0;
            for (int i = 0; i < orderInfos.Length; i++)
            {
                sum += orderInfos[i].BuildStandart.GetComonPrice() * orderInfos[i].CountOfServicesRendered;
            }
            return sum;
        }




    }
}
