using Microsoft.AspNetCore.Mvc;
using PL.Infrastructure.Services.Abstract;
using PL.Infrastructure.Sorting;
using PL.Models;
using PL.Models.ModelsForView;
using System.Collections.Generic;
using System.Linq;

namespace PL.Controllers
{

    public class ProductController : Controller
    {
        private int pageSize = 6;
        private IBuildStandartService _repository;
        public ProductController(IBuildStandartService repository)
        {
            _repository = repository;
        }

        public ViewResult List(int page = 1, decimal? maxPrice=decimal.MaxValue, decimal?minPrice=decimal.MinValue, int service=0, int component=0, BuildStandartSortState sortState = BuildStandartSortState.ComponentTitleAsc)
        {
            IEnumerable<BuildStandart> buildStandarts = _repository.Read();
            
            if (maxPrice!=null && maxPrice != decimal.MaxValue)
            {
                buildStandarts = buildStandarts.Where(x => x.Componet.Price + x.Service.Price < maxPrice);
            }
            if (service != 0)
            {
                buildStandarts = buildStandarts.Where(x => x.Service.Id == service);
            }
            if (component != 0)
            {
                buildStandarts = buildStandarts.Where(x => x.Componet.Id == component);
            }

            if (maxPrice != null && maxPrice != decimal.MinValue)
            {
                buildStandarts = buildStandarts.Where(x => x.Componet.Price + x.Service.Price > minPrice);
            }

            switch(sortState)
            {
                case BuildStandartSortState.ServiceTitleAsc:
                    buildStandarts = buildStandarts.OrderBy(x => x.Service.Title);
                    break;
                case BuildStandartSortState.PriceAsc:
                    buildStandarts = buildStandarts.OrderBy(x => x.Service.Price + x.Componet.Price);
                    break;
                case BuildStandartSortState.ServiceTitleDes:
                    buildStandarts = buildStandarts.OrderByDescending(x => x.Service.Title);
                    break;
                case BuildStandartSortState.PriceDes:
                    buildStandarts = buildStandarts.OrderByDescending(x => x.Service.Price + x.Componet.Price);
                    break;
                case BuildStandartSortState.ComponentTitleDes:
                    buildStandarts = buildStandarts.OrderByDescending(x => x.Componet.Title);
                    break;
                default:
                    buildStandarts = buildStandarts.OrderBy(x => x.Componet.Title);
                    break;
            };
            
            int count = buildStandarts.Count();
            var Items = buildStandarts.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            
            BuilderStandartListViewModel res = new BuilderStandartListViewModel()
            {
                Standarts = Items,
                PageViewModel = new PageViewModel(count, page, pageSize),
                SortViewModel = new BuildStandartSortViewModel(sortState),
                FilterViewModel = new BuilderStabdartFilterViewModel(Items, component, service,minPrice, maxPrice)
            };
            return View(res);
        }
    }
}
