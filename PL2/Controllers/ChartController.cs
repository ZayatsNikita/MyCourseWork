using Microsoft.AspNetCore.Mvc;
using PL.Infrastructure.Extensions;
using PL.Infrastructure.Services.Abstract;
using PL.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Web.Helpers;
using PL.Models.ModelsForView;

namespace PL.Controllers
{
    public class ChartController : Controller
    {
        private IChartManager _service;
        public ChartController(IChartManager service)
        {
            _service = service;
        }
        public ActionResult ProfitByMasters(TimeInterval interval)
        {
            if(interval.From!=null && interval.To != null)
            {
                if (interval.From > interval.To)
                {
                    ModelState.AddModelError("interval", "Invalid time specified");
                }
            }
            if (ModelState.IsValid)
            {
                var data = _service.GetInformationAboutProfitByMasters(interval.From, interval.To);
                //ViewData["Manager"] = data.Select(x => x.Key.PersonalData);
                //ViewBag.M = new List<string>() { "Denis","Kiril", "Valera","Nikita"}.ToArray();
                ////ViewData["Profit"] = data.Select(y => y.Value);
                //ViewBag.P = new List<decimal>() {13, 12, 25, 50 }.ToArray();

                ViewBag.M = new List<string>() { "Denis", "Kiril", "Valera", "Nikita", "Larisa", "Victor", "Vadim", "Egor" }.ToArray();
                ViewBag.P = new List<decimal>() { 13, 12, 25, 50, 39, 20, 32, 15 }.ToArray();
            }
            else
            {
                return View(interval);
            }
            return View();
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

            ViewBag.M = new List<string>() { "Denis", "Kiril", "Valera", "Nikita", "Larisa", "Victor", "Vadim", "Egor" }.ToArray();
            ViewBag.P = new List<decimal>() { 13, 12, 25, 50, 39, 20, 32, 15 }.ToArray();
        
            return View();
        }

        public ActionResult ProfitByManager(DateTime? from, DateTime? to)
        {
            ViewBag.M = new List<string>() { "Kiril", "Valera" }.ToArray();
            ViewBag.P = new List<decimal>() { 13, 12 }.ToArray();
            return View();
        }
    }
}
