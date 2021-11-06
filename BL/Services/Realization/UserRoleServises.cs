using AutoMapper;
using BL.DtoModels;
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

        public int Create(UserRole userRole) => _repository.Create(_mapper.Map<UserRole, UserRoleEntity>(userRole));

        public void Delete(int id) => _repository.Delete(id);

        public List<UserRole> Read() => _mapper.Map<List<UserRoleEntity>, List<UserRole>>(_repository.Read());

        public UserRole ReadById(int id) => _mapper.Map<UserRoleEntity, UserRole>(_repository.ReadById(id));

        public void Update(UserRole userRole) => _repository.Update(_mapper.Map<UserRole, UserRoleEntity>(userRole));
    }
}
