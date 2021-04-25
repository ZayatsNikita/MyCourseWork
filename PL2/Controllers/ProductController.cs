using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PL.Infrastructure.Services.Abstract;
using PL.Models;
using PL.Infrastructure.Sorting;
namespace PL.Controllers
{
    public class ProductController : Controller
    {
        private IBuildStandartService _repository;
        public ProductController(IBuildStandartService repository)
        {
            _repository = repository;
        }

        public ViewResult List(int? serviceId, int? componentId, BuildStandartSortState sortState = BuildStandartSortState.ComponentTitleAsc)
        {
            ViewData["Price"] = sortState == BuildStandartSortState.PriceAsc ?  BuildStandartSortState.PriceDes : BuildStandartSortState.PriceAsc;
            ViewData["ComponentTitleSort"] = sortState == BuildStandartSortState.ComponentTitleAsc ? BuildStandartSortState.ComponentTitleDes : BuildStandartSortState.ComponentTitleAsc;
            ViewData["ServiceTitleSort"] = sortState == BuildStandartSortState.ServiceTitleAsc ? BuildStandartSortState.ServiceTitleDes : BuildStandartSortState.ServiceTitleAsc;
            List <BuildStandart> buildStandarts = _repository.Read();
            buildStandarts = sortState switch
            {
                BuildStandartSortState.ServiceTitleAsc => buildStandarts.OrderBy(x => x.Service.Title).ToList(),
                BuildStandartSortState.PriceAsc => buildStandarts.OrderBy(x => x.Service.Price+x.Componet.Price).ToList(),
                BuildStandartSortState.ServiceTitleDes => buildStandarts.OrderByDescending(x => x.Service.Title).ToList(),
                BuildStandartSortState.PriceDes => buildStandarts.OrderByDescending(x => x.Service.Price + x.Componet.Price).ToList(),
                BuildStandartSortState.ComponentTitleDes => buildStandarts.OrderByDescending(x => x.Componet.Title).ToList(),
                _ => buildStandarts.OrderBy(x => x.Componet.Title).ToList()

            };

            return View(buildStandarts);
        }
    }
}
