using Microsoft.AspNetCore.Mvc;
using PL.Infrastructure.Services.Abstract;
using PL.Models.ModelsForView;
using System;
using Microsoft.AspNetCore.Http;
using PL.Infrastructure.Extensions;

namespace PL.Controllers
{
    public class AcountController : Controller
    {
        private IFullUserServices _fullUserServices;
        public AcountController(IFullUserServices fullUserServices)
        {
            _fullUserServices = fullUserServices;
        }

        [HttpPost]
        public ActionResult StartRoleChoosing(string login, string password)
        {
            FullUser fullUser = null;
            try
            {
                fullUser = _fullUserServices.Read(login, password);
            }
            catch (ArgumentException)
            {
                return RedirectToAction(nameof(Index), new { attend = 1 });
            }
            SaveUserData(fullUser);
            return RedirectToAction("RoleChoosing");
        }
        public ActionResult RoleChoosing()
        {
            FullUser sessionUser = GetUserData();
            return View(sessionUser);
        }

        public ActionResult Index(int attend = 0)
        {
            if(attend != 0)
            {
                ViewBag.Mes = "Wrong login or password";
            }
            return View();
        }
        private void SaveUserData(FullUser fullUser)
        {
            HttpContext.Session.SetJson("user", fullUser);
        }
        private FullUser GetUserData()
        {
            FullUser user = HttpContext.Session.GetJson<FullUser>("user");
            return user;
        }
    }
}
