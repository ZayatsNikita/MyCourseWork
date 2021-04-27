using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace PL.Models.ModelsForView
{
    public class BuilderStabdartFilterViewModel
    {
        public BuilderStabdartFilterViewModel(List<BuildStandart> standarts, int? component,
            int? service, decimal? minValue, decimal? maxValue)
        {
            var componentList = standarts.Select(x => x.Componet).Distinct().ToList();
            var serviceList = standarts.Select(x => x.Service).Distinct().ToList();

            componentList.Insert(0, new Componet() { Id = 0, Title = "All" });
            serviceList.Insert(0, new Service() { Id = 0, Title = "All" });

            Components = new SelectList(componentList, "Id", "Title", component);
            Services = new SelectList(serviceList, "Id", "Title", service);

            MinValue = minValue;
            MaxValue = maxValue;

            SelectedComponent = component;
            SelectedService = service;
        }
        public SelectList Components { get; private set; }
        public SelectList Services { get; private set; }
        
        public int? SelectedService { get; set; }
        public int? SelectedComponent { get; set; }
        public decimal? MinValue { get; set; }
        public decimal? MaxValue { get; set; }
    }
}
