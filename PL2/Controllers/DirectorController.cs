using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PL.Infrastructure.Extensions;
using PL.Infrastructure.Services.Abstract;
using PL.Models;
using PL.Models.ModelsForView;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PL.Controllers
{
    public class DirectorController : Controller
    {
        private IWorkerServices _workerServises;
        private IFullUserServices _fullUserServices;
        private IRoleServices _roleServices;
        public DirectorController(IWorkerServices workerServices, IFullUserServices fullUserServices, IRoleServices roleServices)
        {
            _workerServises = workerServices;
            _roleServices = roleServices;
            _fullUserServices = fullUserServices;
        }
        public ActionResult StartMenu()
        {
            return View("StartMenu");
        }
    }
}
