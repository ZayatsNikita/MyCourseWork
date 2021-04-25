using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class ManagerController : Controller
    {
        public IActionResult StartMenu()
        {
            return View();
        }

        
        
    }
}
