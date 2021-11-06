using AutoMapper;
using BL.DtoModels;
using DL.Entities;
using DL.Repositories.Abstract;
using System.Collections.Generic;

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
       
        public int Create(Role role) => _repository.Create(_mapper.Map<Role, RoleEntity>(role));

        public void Delete(int id) => _repository.Delete(id);
        
        public List<Role> Read() => _mapper.Map<List<RoleEntity>, List<Role>>(_repository.Read());

        public Role ReadById(int id) => _mapper.Map<RoleEntity, Role>(_repository.ReadById(id));

        public void Update(Role role) => _repository.Update(_mapper.Map<Role, RoleEntity>(role));
    }
}
