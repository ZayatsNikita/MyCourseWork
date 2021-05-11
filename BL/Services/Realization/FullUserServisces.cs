using BL.DtoModels;
using BL.DtoModels.Combined;
using BL.Services.Abstract;
using BL.Services.Validaton;
using System;
using System.Collections.Generic;
using System.Linq;
namespace BL.Services
{
    public class FullUserServisces : IFullUserServices
    {
        private IUserServices _userServices;
        private IWorkerServices _workerServices;
        private IRoleServices _roleServices;
        private IUserRoleServices _userRoleServices;
        public FullUserServisces(IUserServices userServices, IWorkerServices worekerServices, IRoleServices roleServices, IUserRoleServices userRoleServices)
        {
            _roleServices = roleServices;
            _userServices = userServices;
            _workerServices = worekerServices;
            _userRoleServices = userRoleServices;
        }

        public void Create(FullUser fullUser)
        {
            if (    
                fullUser.IsValid() &&
                fullUser.Worker.IsValid(_workerServices.Read().Select(x=>x.PassportNumber).ToList()) &&
                (fullUser?.User?.IsValid(_userServices.Read().Select(y=>y.Login).ToList()) ?? true)
                )
            {
                try
                {
                    _workerServices.Create(fullUser.Worker);
                }
                catch (Exception)
                {
                    throw;
                }
                if (fullUser.User != null)
                {
                    fullUser.User = _userServices.Create(fullUser.User);
                    
                    foreach (var item in fullUser.Roles)
                    {
                        _userRoleServices.Create(new UserRole() { UserId = fullUser.User.Id, RoleId = item.Id });
                    }
                }
            }

        }

        public void Delete(FullUser fullUser)
        {
            _workerServices.Delete(fullUser.Worker);
        }

        public FullUser Read(string login, string password)
        {
            if (login == null || password == null)
            {
                login = "";
                password = "";
            }
            List<User> users = _userServices.Read(login: login, password: password);
            if (users.Count == 0)
            {
                throw new ArgumentException();
            }
            List<Worker> workers = _workerServices.Read(minPassportNumber: users[0].WorkerId, maxPassportNumber: users[0].WorkerId);
            List<UserRole> userRoles = _userRoleServices.Read(minUserId: users[0].Id, maxUserId: users[0].Id);
            List<Role> roles = new List<Role>();
            foreach (UserRole item in userRoles)
            {
                var roleList = _roleServices.Read(minId: item.RoleId, maxId: item.RoleId);
                roles.Add(roleList[0]);
            }
            FullUser result = new FullUser { User = users[0], Worker = workers[0], Roles = roles };
            return result;
        }
        public FullUser Read(int workerNumber)
        {
            List<Worker> workers = _workerServices.Read(minPassportNumber: workerNumber, maxPassportNumber: workerNumber);
            List<User> users = _userServices.Read(workerId: workerNumber);
            if (users.Count == 0)
            {
                return new FullUser { Worker = workers[0], User = null, Roles = new List<Role>() };
            }
            else
            {
                List<UserRole> userRoles = _userRoleServices.Read(minUserId: users[0].Id, maxUserId: users[0].Id);
                List<Role> roles = new List<Role>();
                foreach (UserRole item in userRoles)
                {
                    var roleList = _roleServices.Read(minId: item.RoleId, maxId: item.RoleId);
                    roles.Add(roleList[0]);
                }
                FullUser result = new FullUser { User = users[0], Worker = workers[0], Roles = roles };
                return result;
            }
        }

        public void Update(FullUser fullUser)
        {
            if (fullUser.IsValid())
            {
                _workerServices.Update(fullUser.Worker, fullUser.Worker.PersonalData);

                _userServices.Delete(new User(), fullUser.Worker.PassportNumber);

                if (fullUser.User != null)
                {
                    fullUser.User = _userServices.Create(fullUser.User);

                    int length = fullUser.Roles.Count;
                    for (int i = 0; i < length; i++)
                    {
                        UserRole userRole = new UserRole() { RoleId = fullUser.Roles[i].Id, UserId = fullUser.User.Id };
                        _userRoleServices.Create(userRole);
                    }
                }
            }
            
        }
    }
}
