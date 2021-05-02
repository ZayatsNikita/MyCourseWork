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
    public class AdminController : Controller
    {
        private IWorkerServices _workerServises;
        private IFullUserServices _fullUserServices;
        private IRoleServices _roleServices;
        public AdminController(IWorkerServices workerServices, IFullUserServices fullUserServices, IRoleServices roleServices)
        {
            _workerServises = workerServices;
            _roleServices = roleServices;
            _fullUserServices = fullUserServices;
        }
        public ActionResult StartMenu()
        {
            return View("StartMenu");
        }
        public ActionResult EditWorkersInformation()
        {
            FullUser user = HttpContext.Session.GetJson<FullUser>("user");
            List<Worker> workers = _workerServises.Read().Where(x=>x.Equals(user.Worker)==false).ToList();
            return View("WorkersList", workers);
        }
        private FullUser GetUserData()
        {
            FullUser user = HttpContext.Session.GetJson<FullUser>("user");
            return user;
        }
        [HttpPost]
        public ActionResult DeleteWorker(int WorkerNumber)
        {
            _workerServises.Delete(new Worker {PassportNumber = WorkerNumber });
            return RedirectToAction("EditWorkersInformation");
        }
        public ActionResult EditWorker(int workerNumber) =>
            View(_fullUserServices.Read(workerNumber).ConvertToChenged(_roleServices.Read()));

        public IActionResult CreateWorker() => View(new FullUser().ConvertToChenged(_roleServices.Read()));

        [HttpPost]
        public ActionResult CreateWorker(FullUserThatAllowsChanges changedUser)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    FullUser fullUser = changedUser.ConvertToOrdinarFullUser(_roleServices.Read());
                    _fullUserServices.Create(fullUser);
                    return RedirectToAction("EditWorkersInformation");
                }
                catch (BL.Exceptions.ValidationException ex)
                {
                    ModelState.AddModelError(ex.GetHashCode().ToString(), ex.Message);
                }
            }
            return View(changedUser);
        }

        [HttpPost]
        public ActionResult EditWorker(FullUserThatAllowsChanges changedUser)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    FullUser fullUser = changedUser.ConvertToOrdinarFullUser(_roleServices.Read());
                    _fullUserServices.Update(fullUser);
                    return RedirectToAction("EditWorkersInformation");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(ex.GetHashCode().ToString(),ex.Message);
                }
            }
            
            return View(changedUser);
            
        }
    }
}
