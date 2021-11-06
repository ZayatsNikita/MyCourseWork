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
    public class MasterController : Controller
    {
        private IServiceServices _serviceServices;
        private IBuildStandartService _buildStandartServices;
        private IComponentServices _componentServices;
        public MasterController(IServiceServices serviceServices, IComponentServices componentServices, IBuildStandartService serviceComponetServices)
        {
            _serviceServices = serviceServices;
            _componentServices = componentServices;
            _buildStandartServices = serviceComponetServices;
        }
        public ActionResult StartMenu()
        {
            FullUser user = new FullUser();
            user.GetUserFromCookie(HttpContext);
            ViewBag.Data = user.Worker.PassportNumber;
            return View("StartMenu");
        }

        #region управление сервисами
        public ActionResult ServicesList() 
        {
            List<Service> services = _serviceServices.Read();
            return View(services);
        }
        
        public ActionResult EditService(int serviceId) =>
            View(_serviceServices.ReadById(serviceId));

        [HttpPost]
        public ActionResult EditService(Service service)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _serviceServices.Update(service);
                    return RedirectToAction("ServicesList");
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError(ex.GetHashCode().ToString(), ex.Message);
                }
            }
            return View(service);
        }

        public ActionResult CreateService() =>
            View(new Service());

        [HttpPost]
        public ActionResult CreateService(Service service)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _serviceServices.Create(service);
                    return RedirectToAction("ServicesList");
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError(ex.GetHashCode().ToString(), ex.Message);
                }
            }
            return View(service);
            
        }
        
        public ActionResult DeleteService(int serviceId)
        {
            _serviceServices.Delete(new Service() {Id=serviceId });
            return RedirectToAction("ServicesList");
        }
        #endregion

        #region Управление узлами/деталями
        public ActionResult ComponentList()
        {
            List<Componet> components = _componentServices.Read();
            return View(components);
        }
        public ActionResult EditComponent(int componentId) =>
            View(_componentServices.ReadById(componentId));
        
        [HttpPost]
        public ActionResult EditComponent(Componet component)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _componentServices.Update(component);
                    return RedirectToAction("ComponentList");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(ex.GetHashCode().ToString(), ex.Message);
                }
            }
            return View(component);
        }
        public ActionResult CreateComponent() =>
            View(new Componet());

        [HttpPost]
        public ActionResult CreateComponent(Componet componet)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _componentServices.Create(componet);
                    return RedirectToAction("ComponentList");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(ex.GetHashCode().ToString(), ex.Message);
                }
            }
            return View(componet);
        }
        public ActionResult DeleteComponent(int componentId)
        {
            _componentServices.Delete(new Componet() { Id = componentId });
            return RedirectToAction("ComponentList");
        }

        #endregion

        #region Управление нормами сборки
        public ActionResult BuildStandardsList()
        {
            List<BuildStandart> standarts = _buildStandartServices.Read();
            return View(standarts);
        }
        public ActionResult CreateBuildStandards()
        {
          ViewBag.Services = _serviceServices.Read();
          ViewBag.Components = _componentServices.Read(); 
          return View(new BuildStandart());
        }

        [HttpPost]
        public ActionResult CreateBuildStandards(BuildStandart standart)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _buildStandartServices.Create(standart);
                    return RedirectToAction("BuildStandardsList");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(ex.GetHashCode().ToString(), ex.Message);
                }
            }
            ViewBag.Services = _serviceServices.Read();
            ViewBag.Components = _componentServices.Read();
            return View(standart);
        }
        public ActionResult DeleteBuildStandards(int builderStandartId)
        {
            _buildStandartServices.Delete(new BuildStandart() { Id = builderStandartId });
            return RedirectToAction(nameof(BuildStandardsList));
        }

        #endregion
    }
}
