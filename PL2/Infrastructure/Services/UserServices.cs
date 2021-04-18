using AutoMapper;
using PL.Models;
using System.Collections.Generic;

namespace PL.Infrastructure.Services
{
    public class UserServices : Abstract.IUserServices
    {
        private BL.Services.Abstract.IUserServices _servises;
        private Mapper _mapper;
        public UserServices(BL.Services.Abstract.IUserServices servises, Mapper mapper)  
        {
            _mapper = mapper;
            _servises = servises; 
        }

        public void Create(User user)
        {
            _servises.Create(_mapper.Map<User, BL.dtoModels.User>(user));
        }

        public void Delete(User user)
        {
            _servises.Delete(_mapper.Map<User, BL.dtoModels.User>(user));

        }

        public List<User> Read(int minId, int maxId, string login, string password, int workerId)
        {
            List<User> result = _mapper.Map<List<BL.dtoModels.User>, List<User>> (_servises.Read(minId,  maxId, login, password, workerId));
            return result;

        }

        public void Update(User user, string login, string password, int workerId)
        {
            _servises.Update(_mapper.Map<User, BL.dtoModels.User>(user), login, password, workerId);
        }
    }

   


}
