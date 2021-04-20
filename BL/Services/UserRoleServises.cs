using AutoMapper;
using BL.dtoModels;
using DL.Entities;
using DL.Repositories.Abstract;
using System.Collections.Generic;

namespace BL.Services
{
    public class UserRoleServises : Abstract.IUserRoleServices
    {
        private IUserRoleRepository _repository;
        private Mapper _mapper;
        public UserRoleServises(IUserRoleRepository repository, Mapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public void Create(UserRole userRole)
        {
            _repository.Create(_mapper.Map<UserRole, UserRoleEntity>(userRole));
        }

        public void Delete(UserRole userRole)
        {
            _repository.Delete(_mapper.Map<UserRole, UserRoleEntity>(userRole));
        }

        public List<UserRole> Read(int minId, int maxId, int minUserId, int maxUserId, int minRoleId, int maxRoleId)
        {
            List<UserRole> result = _mapper.Map<List<UserRoleEntity>, List<UserRole>>(_repository.Read(minId, maxId, minUserId, maxUserId, minRoleId, maxRoleId));
            return result;
        }

        public void Update(UserRole userRole, int userId, int roleId)
        {
            _repository.Update(_mapper.Map<UserRole, UserRoleEntity>(userRole),userId, roleId);
        }
    }
}
