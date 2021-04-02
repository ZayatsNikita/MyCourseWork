using System;
using System.Collections.Generic;
using System.Text;
using DL.Repositories.Abstract;
using BL.dtoModels;
using BL.Mappers;
using AutoMapper;
using DL.Entities;

namespace BL.Services
{
    public class RoleServices : Abstract.IRoleServices
    {
        private IRoleEntityRepository _repository;
        private Mapper _mapper;
        public RoleServices(IRoleEntityRepository repository, Mapper mapper)  
        {
            _mapper = mapper;
            _repository = repository;
        }
       
        public void Create(Role role)
        {
            _repository.Create(_mapper.Map<Role, RoleEntity>(role));
        }

        public void Delete(Role role)
        {
            _repository.Delete(_mapper.Map<Role, RoleEntity>(role));
        }
        public List<Role> Read(int minId, int maxId, string title, string description, int userId)
        {
            List<Role> result = _mapper.Map<List<RoleEntity>,List<Role>>(_repository.Read(minId, maxId, title, description, userId));
            return result;
        }

        public void Update(Role role, string title, string description, int userId)
        {
            _repository.Update(_mapper.Map<Role, RoleEntity>(role), title, description ,userId);
        }
    }

   


}
