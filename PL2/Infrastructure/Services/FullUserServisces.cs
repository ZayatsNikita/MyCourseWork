using PL.Models;
using PL.Models.ModelsForView;
using System.Collections.Generic;
using PL.Infrastructure.Services.Abstract;
using System;

namespace PL.Infrastructure.Services
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
            throw new NotImplementedException();
        }

        public void Delete(FullUser fullUser)
        {
            _workerServices.Delete(new Worker {PassportNumber = fullUser.Worker.PassportNumber });
        }

        public FullUser Read(string login, string password)
        {
            List<User> users = _userServices.Read(login: login, password: password);
            if(users.Count == 0)
            {
                throw new ArgumentException();
            }
            List<Worker> workers = _workerServices.Read(minPassportNumber: users[0].WorkerId, maxPassportNumber: users[0].WorkerId);
            List <UserRole> userRoles = _userRoleServices.Read(minUserId: users[0].Id, maxUserId: users[0].Id);
            List<Role> roles = new List<Role>();
            foreach (UserRole item in userRoles)
            {
                var roleList = _roleServices.Read(minId: item.RoleId, maxId:item.RoleId);
                roles.Add(roleList[0]);
            }
            FullUser result = new FullUser {User = users[0], Worker = workers[0], Roles = roles };
            return result;
        }

        public FullUser Read(int workerNumber)
        {
            
        }

        public void Update(FullUser user)
        {
            throw new NotImplementedException();
        }
    }
}
