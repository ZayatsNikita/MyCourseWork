using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PL.Models
{
    public class Cart
    {
        public Cart()
        {
            OrderLine = new Dictionary<int, Line>();
        }
        public Dictionary<int, Line> OrderLine { get; set; }

        public void Add(int count, BuildStandart buildStandart)
        {
            if (OrderLine.ContainsKey(buildStandart.Id))
            {
                Line line = OrderLine[buildStandart.Id];
                line.Count += count;
            }
            else
            {
                Line line = new Line() { Count = count, BuildStandart = buildStandart };
                OrderLine.Add(buildStandart.Id, line);
            }
        }
        public void Remove(int builderStandertId)
        {
            OrderLine.Remove(builderStandertId);
        }
        public void Clear()
        {
            OrderLine.Clear();
        }
        public decimal Sum()
        {
            decimal sum = OrderLine.Sum(x => x.Value.GetPrice());
            return sum;
        }
    }
    public class Line
    {
        public BuildStandart BuildStandart { get; set; }
        public int Count { get; set; }
        public decimal GetPrice()
        {
            return Count * (BuildStandart.Componet.Price + BuildStandart.Service.Price);
        }
    }
}
