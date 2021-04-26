﻿using AutoMapper;
using BL.DtoModels;
using DL.Entities;
using DL.Repositories.Abstract;
using System.Collections.Generic;

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
            User result =_mapper.Map< UserEntity, User>(_repository.Create(_mapper.Map<User, UserEntity>(user)));
            return result;
        }

        public void Delete(User user, int workerId = -1)
        {
            _repository.Delete(_mapper.Map<User, UserEntity>(user), workerId);
        }

        public List<User> Read(int minId = -1, int maxId=-1, string login = null, string password = null, int workerId = -1)
        {
            List<User> result = _mapper.Map<List<UserEntity>, List<User>> (_repository.Read(minId,  maxId, login, password, workerId));
            return result;

        }

        public void Update(User user, string login = null, string password = null, int workerId = -1)
        {
            _repository.Update(_mapper.Map<User, UserEntity>(user), login, password, workerId);
        }
    }

   


}