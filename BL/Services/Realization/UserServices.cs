using AutoMapper;
using BL.DtoModels;
using BL.Services.Validaton;
using DL.Entities;
using DL.Repositories.Abstract;
using System.Collections.Generic;
using System.Linq;
namespace BL.Services
{
    public class UserServices : Abstract.IUserServices
    {
        private IUserEntityRepo _repository;
        private Mapper _mapper;
        public UserServices(IUserEntityRepo repository, Mapper mapper)  
        {
            _mapper = mapper;
            _repository = repository; 
        }

        public User Create(User user)
        {
            if (user.IsValid(_repository.Read().Select(x=>x.Login).ToList()))
            {
                User result = _mapper.Map<UserEntity, User>(_repository.Create(_mapper.Map<User, UserEntity>(user)));
                return result;
            }
            throw new System.ArgumentException("The data is not correct");
        }

        public void Delete(User user, int workerId = Constants.DefIntVal)
        {
            _repository.Delete(_mapper.Map<User, UserEntity>(user), workerId);
        }

        public List<User> Read(int minId = Constants.DefIntVal, int maxId= Constants.DefIntVal, string login = null, string password = null, int workerId = Constants.DefIntVal)
        {
            List<User> result = _mapper.Map<List<UserEntity>, List<User>> (_repository.Read(minId,  maxId, login, password, workerId));
            return result;

        }

        public void Update(User user, string login = null, string password = null, int workerId = Constants.DefIntVal)
        {
            user.Login = login;
            if (user.IsValid(_repository.Read().Select(x => x.Login).Where(x=>x!=user.Login).ToList()))
            {
                _repository.Update(_mapper.Map<User, UserEntity>(user), login, password, workerId);
            }
        }
    }

   


}
