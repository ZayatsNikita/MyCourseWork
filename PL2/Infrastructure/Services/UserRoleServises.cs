using AutoMapper;
using PL.Models;
using System.Collections.Generic;

namespace PL.Infrastructure.Services
{
    public class UserRoleServises : Abstract.IUserRoleServices
    {
        private BL.Services.Abstract.IUserRoleServices _servisec;
        private Mapper _mapper;
        public UserRoleServises(BL.Services.Abstract.IUserRoleServices servisec, Mapper mapper)
        {
            _mapper = mapper;
            _servisec = servisec;
        }
        public void Create(UserRole userRole)
        {
            _servisec.Create(_mapper.Map<UserRole, BL.DtoModels.UserRole>(userRole));
        }

        public void Delete(UserRole userRole)
        {
            _servisec.Delete(_mapper.Map<UserRole, BL.DtoModels.UserRole>(userRole));
        }

        public List<UserRole> Read(int minId, int maxId, int minUserId, int maxUserId, int minRoleId, int maxRoleId)
        {
            List<UserRole> result = _mapper.Map<List<BL.DtoModels.UserRole>, List<UserRole>>(_servisec.Read(minId, maxId, minUserId, maxUserId, minRoleId, maxRoleId));
            return result;
        }

        public void Update(UserRole userRole, int userId, int roleId)
        {
            _servisec.Update(_mapper.Map<UserRole, BL.DtoModels.UserRole>(userRole),userId, roleId);
        }
    }
}
