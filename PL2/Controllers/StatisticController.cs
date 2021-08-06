using Microsoft.AspNetCore.Mvc;
using PL.Infrastructure.Services.Abstract;
using PL.Models.ModelsForView;
using System.Linq;
using PL.Infrastructure.Enumerators;

namespace PL.Controllers
{
    public class StatisticController : Controller
    {
        private IChartManager _service;
        public StatisticController(IChartManager service)
        {
            _service = service;
        }
        
        public ActionResult ProductionNeed(TimeInterval interval)
        {
            var data = _service.GetInformationAboutTheServicesOrdered(interval.From, interval.To);

            var serviceCount = data.Select(x => new { Count = x.CountOfServicesRendered, x.BuildStandart.Service}).GroupBy(y=>y.Service).Select(z=>new {z.Key.Title, Sum = z.Sum(t=>t.Count) });
            var componenteCount = data.Select(x => new { Count = x.CountOfServicesRendered, x.BuildStandart.Componet}).GroupBy(y=>y.Componet).Select(z=>new {z.Key.Title, Sum = z.Sum(t=>t.Count) });

            ViewBag.SC = serviceCount.Select(z => z.Sum);
            ViewBag.ST = serviceCount.Select(z => z.Title);

            ViewBag.CC = componenteCount.Select(z => z.Sum);
            ViewBag.CT = componenteCount.Select(z => z.Title);

            if(interval != null && interval.From != null && interval.To != null)
            {
                if (interval.From > interval.To)
                {
                    ModelState.AddModelError("", "The dates are in the wrong order.");
                }
            }
        
            return View();
        }
        public ActionResult ProfitByWorkers(TimeInterval interval)
        {
            var dataManagers = _service.GetInformationAboutProfitByManagers(interval.From, interval.To);
            var dataMasters = _service.GetInformationAboutProfitByMasters(interval.From, interval.To);

            ViewBag.SC = dataManagers.Select(z => z.Value);
            ViewBag.ST = dataManagers.Select(z => z.Key.PersonalData);
            
            ViewBag.CC = dataMasters.Select(z => z.Value);
            ViewBag.CT = dataMasters.Select(z => z.Key.PersonalData);

            if (interval != null && interval.From != null && interval.To != null)
            {
                if (interval.From > interval.To)
                {
                    ModelState.AddModelError("", "The dates are in the wrong order.");
                }
            }
            return View();

        }
        
        public ActionResult ProductionNeedTableForm(TimeInterval interval, ComponentSortEnum componentSort = ComponentSortEnum.TitleAsc, ServiceSortEnum serviceSort=ServiceSortEnum.TitleAsc)
        {
            var data = _service.GetInformationAboutTheServicesOrdered(interval.From, interval.To);
            #region выбираю способ сотироваки
            ViewData["CompTitleS"] = componentSort == ComponentSortEnum.TitleAsc ? ComponentSortEnum.TitleDes : ComponentSortEnum.TitleAsc;
            ViewData["CompQauntS"] = componentSort == ComponentSortEnum.CountAsc ? ComponentSortEnum.CountDes : ComponentSortEnum.CountAsc;
            ViewData["CompStandS"] = componentSort == ComponentSortEnum.StandartAsc ? ComponentSortEnum.StandartDes : ComponentSortEnum.StandartAsc;

            ViewData["ServiceTitleS"] = serviceSort == ServiceSortEnum.TitleAsc ? ServiceSortEnum.TitleDes : ServiceSortEnum.TitleAsc;
            ViewData["ServiceQauntS"] = serviceSort == ServiceSortEnum.CountAsc ? ServiceSortEnum.CountDes : ServiceSortEnum.CountAsc;
            #endregion

            #region 
            var services = data
                .Select(x => new { Count = x.CountOfServicesRendered, x.BuildStandart.Service })
                .GroupBy(y => y.Service)
                .Select(z => new { z.Key.Title, Sum = z.Sum(t => t.Count) });
            
            var componentes = data.
                Select(x => new { Count = x.CountOfServicesRendered, x.BuildStandart.Componet })
                .GroupBy(y => y.Componet)
                .Select(z => new { z.Key.Title,z.Key.ProductionStandards, Sum = z.Sum(t => t.Count) });
            #region сортировка
            switch (serviceSort)
            {
                case ServiceSortEnum.CountAsc:
                    services = services.OrderBy(x => x.Sum);
                    break;
                case ServiceSortEnum.CountDes:
                    services = services.OrderByDescending(x => x.Sum);
                    break;
                case ServiceSortEnum.TitleAsc:
                    services = services.OrderBy(x => x.Title);
                    break;
                case ServiceSortEnum.TitleDes:
                    services = services.OrderByDescending(x => x.Title);
                    break;
            }
            switch (componentSort)
            {
                case ComponentSortEnum.CountAsc:
                    componentes = componentes.OrderBy(x => x.Sum);
                    break;
                case ComponentSortEnum.CountDes:
                    componentes = componentes.OrderByDescending(x => x.Sum);
                    break;
                case ComponentSortEnum.TitleAsc:
                    componentes = componentes.OrderBy(x => x.Title);
                    break;
                case ComponentSortEnum.TitleDes:
                    componentes = componentes.OrderByDescending(x => x.Title);
                    break;
                case ComponentSortEnum.StandartAsc:
                    componentes = componentes.OrderBy(x => x.ProductionStandards);
                    break;
                case ComponentSortEnum.StandartDes:
                    componentes = componentes.OrderByDescending(x => x.ProductionStandards);
                    break;
            }
            #endregion


            ViewBag.Length = componentes.Count();
            ViewBag.ServiceLen = services.Count();
            
            ViewBag.ST = services.Select(x=>x.Title).ToArray();
            ViewBag.SC = services.Select(x=>x.Sum).ToArray();
            
            ViewBag.CT = componentes.Select(x=>x.Title).ToArray();
            ViewBag.CC = componentes.Select(x=>x.Sum).ToArray();
            ViewBag.CP = componentes.Select(x=>x.ProductionStandards).ToArray();
            
            

            ViewBag.ServieSortValue = serviceSort;
            ViewBag.ComponentSortValue = componentSort;

            if (interval != null && interval.From != null && interval.To != null)
            {
                if (interval.From > interval.To)
                {
                    ModelState.AddModelError("", "The dates are in the wrong order.");
                }
            }
            #endregion
            return View();
        }

        public ActionResult ProfitByWorkersTableForm(TimeInterval interval, WorkerSortEnum masterSort = WorkerSortEnum.NameAsc, WorkerSortEnum managerSort = WorkerSortEnum.NameAsc)
        {
            var dataManagers = _service.GetInformationAboutProfitByManagers(interval.From, interval.To).ToList();
            var dataMasters = _service.GetInformationAboutProfitByMasters(interval.From, interval.To).ToList();

            ViewData["MasterTitleS"] = masterSort == WorkerSortEnum.NameAsc ? WorkerSortEnum.NameDes : WorkerSortEnum.NameAsc;
            ViewData["MasterQauntS"] = masterSort == WorkerSortEnum.ProfitAsc ? WorkerSortEnum.ProfitDes : WorkerSortEnum.ProfitAsc;
            
            ViewData["ManagerTitleS"] = managerSort == WorkerSortEnum.NameAsc ? WorkerSortEnum.NameDes : WorkerSortEnum.NameAsc;
            ViewData["ManagerQauntS"] = managerSort == WorkerSortEnum.ProfitAsc ? WorkerSortEnum.ProfitDes : WorkerSortEnum.ProfitAsc;


            switch (masterSort)
            {
                case WorkerSortEnum.NameAsc:
                    dataMasters = dataMasters.OrderBy(x => x.Key.PersonalData).ToList();
                    break;
                case WorkerSortEnum.NameDes:
                    dataMasters = dataMasters.OrderByDescending(x => x.Key.PersonalData).ToList();
                    break;
                case WorkerSortEnum.ProfitAsc:
                    dataMasters = dataMasters.OrderBy(x => x.Value).ToList();
                    break;
                case WorkerSortEnum.ProfitDes:
                    dataMasters = dataMasters.OrderByDescending(x => x.Value).ToList();
                    break;
            }
            switch (managerSort)
            {
                case WorkerSortEnum.NameAsc:
                    dataManagers = dataManagers.OrderBy(x => x.Key.PersonalData).ToList();
                    break;
                case WorkerSortEnum.NameDes:
                    dataManagers = dataManagers.OrderByDescending(x => x.Key.PersonalData).ToList();
                    break;
                case WorkerSortEnum.ProfitAsc:
                    dataManagers = dataManagers.OrderBy(x => x.Value).ToList();
                    break;
                case WorkerSortEnum.ProfitDes:
                    dataManagers = dataManagers.OrderByDescending(x => x.Value).ToList();
                    break;
            }

            ViewBag.ManagerC = dataManagers.Select(z => z.Value).ToArray();
            ViewBag.ManagerT = dataManagers.Select(z => z.Key.PersonalData).ToArray();

            ViewBag.MasterC = dataMasters.Select(z => z.Value).ToArray();
            ViewBag.MasterT = dataMasters.Select(z => z.Key.PersonalData).ToArray();

            ViewBag.ManagerSortValue = managerSort;
            ViewBag.MasterSortValue = masterSort;

            ViewBag.Length = dataMasters.Count();
            ViewBag.ServiceLen = dataManagers.Count();
            if (interval != null && interval.From != null && interval.To != null)
            {
                if (interval.From > interval.To)
                {
                    ModelState.AddModelError("", "The dates are in the wrong order.");
                }
            }
            return View();
        }
    }
}
