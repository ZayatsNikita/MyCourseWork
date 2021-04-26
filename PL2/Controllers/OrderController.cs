using Microsoft.AspNetCore.Mvc;
using PL.Infrastructure.Extensions;
using PL.Infrastructure.Services.Abstract;
using PL.Models;
using System.Linq;

namespace PL.Controllers
{
    public class OrderController : Controller
    {
        public OrderController() { }
        public IActionResult MakingAnOrder()
        {
            return View();
        }
    }
}
