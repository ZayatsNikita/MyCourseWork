using Microsoft.AspNetCore.Mvc;
using PL.Models.ModelsForView;
using System;
using Microsoft.AspNetCore.Http;
using PL.Infrastructure.Extensions;
using PL.Infrastructure.Services.Abstract;
using DL.Repositories.Realization.MongoDbRepostories;

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
        //    var workerRepository = new MongoDbWorkerRepository();

        //    var userRepository = new MongoDbUserRepository();

        //    var roleRepository = new MongoDbRoleRepository();

        //    var userRoleRepository = new MongoDbUserRolePairsRepository();

            //roleRepository.Create(new DL.Entities.RoleEntity { Title = "Admin", Description = "Main user" });
            //roleRepository.Create(new DL.Entities.RoleEntity { Title = "Master", Description = "Performs repair work" });
            //roleRepository.Create(new DL.Entities.RoleEntity { Title = "Manager", Description = "Performs orders managment" });
            //roleRepository.Create(new DL.Entities.RoleEntity { Title = "Director", Description = "Look to the statistic" });

            //workerRepository.Create(new DL.Entities.WorkerEntity { PassportNumber = 2201207, PersonalData = "Viktor Nikolaevich" });
            //workerRepository.Create(new DL.Entities.WorkerEntity { PassportNumber = 4321123, PersonalData = "Nikolay Vladimirovich" });
            //workerRepository.Create(new DL.Entities.WorkerEntity { PassportNumber = 1427247, PersonalData = "Dmtriy Puchkov" });

            //var userId = userRepository.Create(new DL.Entities.UserEntity { Login = "Login", Password = "Password", WorkerId = 2201207 });

            //userRoleRepository.Create(new DL.Entities.UserRoleEntity { RoleId = 1 , UserId = 2201207 });
            //userRoleRepository.Create(new DL.Entities.UserRoleEntity { RoleId = 2 , UserId = 2201207 });
            //userRoleRepository.Create(new DL.Entities.UserRoleEntity { RoleId = 3 , UserId = 2201207 });
            //userRoleRepository.Create(new DL.Entities.UserRoleEntity { RoleId = 4 , UserId = 2201207 });

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

            return RedirectToAction(nameof(RoleChoosing));
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
