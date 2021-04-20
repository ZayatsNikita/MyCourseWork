using PL.Models;
using PL.Models.ModelsForView;
using System.Collections.Generic;
using PL.Infrastructure.Services.Abstract;
using System;
using AutoMapper;
namespace PL.Infrastructure.Services
{
    #region oldRealization
    //public class FullUserServisces : IFullUserServices
    //{
    //    private IUserServices _userServices;
    //    private IWorkerServices _workerServices;
    //    private IRoleServices _roleServices;
    //    private IUserRoleServices _userRoleServices;
    //    public FullUserServisces(IUserServices userServices, IWorkerServices worekerServices, IRoleServices roleServices, IUserRoleServices userRoleServices)
    //    {
    //        _roleServices = roleServices;
    //        _userServices = userServices;
    //        _workerServices = worekerServices;
    //        _userRoleServices = userRoleServices;
    //    }

    //    public void Create(FullUser fullUser)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void Delete(FullUser fullUser)
    //    {
    //        _workerServices.Delete(new Worker {PassportNumber = fullUser.Worker.PassportNumber });
    //    }

    //    public FullUser Read(string login, string password)
    //    {
    //        List<User> users = _userServices.Read(login: login, password: password);
    //        if(users.Count == 0)
    //        {
    //            throw new ArgumentException();
    //        }
    //        List<Worker> workers = _workerServices.Read(minPassportNumber: users[0].WorkerId, maxPassportNumber: users[0].WorkerId);
    //        List <UserRole> userRoles = _userRoleServices.Read(minUserId: users[0].Id, maxUserId: users[0].Id);
    //        List<Role> roles = new List<Role>();
    //        foreach (UserRole item in userRoles)
    //        {
    //            var roleList = _roleServices.Read(minId: item.RoleId, maxId:item.RoleId);
    //            roles.Add(roleList[0]);
    //        }
    //        FullUser result = new FullUser {User = users[0], Worker = workers[0], Roles = roles };
    //        return result;
    //    }

    //    public FullUser Read(int workerNumber)
    //    {
    //        throw new Exception();
    //    }

    //    public void Update(FullUser user)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
    #endregion

    public class FullUserServisces : IFullUserServices
    {
        private BL.Services.Abstract.IFullUserServices _userServices;
        private Mapper _mapper;
        public FullUserServisces(BL.Services.Abstract.IFullUserServices userServices, Mapper mapper)
        {
            _userServices = userServices;
            _mapper = mapper;
        }

        public void Create(FullUser fullUser)
        {
            _userServices.Create(_mapper.Map<FullUser, BL.dtoModels.Combined.FullUser>(fullUser));
        }

        public void Delete(FullUser fullUser)
        {
            _userServices.Delete(_mapper.Map<FullUser, BL.dtoModels.Combined.FullUser>(fullUser));
        }

        public FullUser Read(string login, string password)
        {
            FullUser result = _mapper.Map<BL.dtoModels.Combined.FullUser, FullUser>(_userServices.Read(login, password));
            return result;
        }

        public FullUser Read(int workerNumber)
        {
            FullUser result = _mapper.Map<BL.dtoModels.Combined.FullUser, FullUser>(_userServices.Read(workerNumber));
            return result;
        }

        public void Update(FullUser fullUser)
        {
            _userServices.Update(_mapper.Map<FullUser, BL.dtoModels.Combined.FullUser>(fullUser));
        }
    }
}
