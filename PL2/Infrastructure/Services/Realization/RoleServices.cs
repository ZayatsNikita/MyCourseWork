using AutoMapper;
using PL.Models;
using System.Collections.Generic;

namespace PL.Infrastructure.Services
{
    public class RoleServices : Abstract.IRoleServices
    {
        private BL.Services.Abstract.IRoleServices _servises;
        
        private Mapper _mapper;

        public RoleServices(BL.Services.Abstract.IRoleServices servises, Mapper mapper)  
        {
            _mapper = mapper;
            this._servises = servises;
        }
       
        public void Create(Role role)
        {
            _servises.Create(_mapper.Map<Role, BL.DtoModels.Role>(role));
        }

        public void Delete(Role role)
        {
            _servises.Delete(role.Id);
        }
        
        public List<Role> Read()
        {
            List<Role> result = _mapper.Map<List<BL.DtoModels.Role>, List<Role>>(_servises.Read());
            return result;
        }

        public void Update(Role role)
        {
            _servises.Update(_mapper.Map<Role, BL.DtoModels.Role>(role));
        }
    }
}
