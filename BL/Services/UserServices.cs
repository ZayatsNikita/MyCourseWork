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
    public class UserServices : Abstract.IUserServices
    {
        private IUserEntityRepo _repository;
        private Mapper _mapper;
        public UserServices(IUserEntityRepo repository, Mapper mapper)  
        {
            _mapper = mapper;
            _repository = repository; 
        }

        public void Create(User user)
        {
            _repository.Create(_mapper.Map<User, UserEntity>(user));
        }

        public void Delete(User user)
        {
            _repository.Delete(_mapper.Map<User, UserEntity>(user));

        }

        public List<User> Read(int minId, int maxId, string login, string password, int workerId)
        {
            List<User> result = _mapper.Map<List<UserEntity>, List<User>> (_repository.Read(minId,  maxId, login, password, workerId));
            return result;

        }

        public void Update(User user, string login, string password, int workerId)
        {
            _repository.Update(_mapper.Map<User, UserEntity>(user), login, password, workerId);
        }
    }

   


}
