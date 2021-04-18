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
        public ActionResult RoleChoosing(string login, string password)
        {
            FullUser fullUser = null;
            try
            {
                fullUser = _fullUserServices.Read(login, password);
            }
            catch (ArgumentException)
            {
                return RedirectToAction(nameof(Index));
            }
            SaveUserData(fullUser);
            return View(fullUser);
        }
        public ActionResult RoleChoosing()
        {
            FullUser sessionUser = GetUserData();;
            return View(sessionUser);
        }

        public ActionResult Index()
        {
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
