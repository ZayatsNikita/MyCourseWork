﻿using System;
using System.Collections.Generic;
using System.Text;
using DL.Repositories.Abstract;
using BL.dtoModels;
using BL.Mappers;
using AutoMapper;
using DL.Entities;

namespace BL.Services
{
    public class ClientServices : Abstract.IClientServices
    {
        private IClientEntiryRepo _repository;
        private Mapper _mapper;
        public ClientServices(IClientEntiryRepo repository, Mapper mapper)  
        {
            _mapper = mapper;
            _repository = repository;
        }
        public void Create(Client client)
        {
            _repository.Create(_mapper.Map<Client, ClientEntity>(client));
        }

        public void Delete(Client client)
        {
            _repository.Delete(_mapper.Map<Client, ClientEntity>(client));
        }

        public List<Client> Read(int MinId=-1, int MaxId=-1, string title=null, string contactInformation = null)
        {
            List<Client> result = _mapper.Map<List<ClientEntity>, List<Client>>(_repository.Read(MinId, MaxId ,title, contactInformation));
            return result;
        }

        public void Update(Client client, string title = null, string contactInformation = null)
        {
            _repository.Update(_mapper.Map<Client, ClientEntity>(client), title, contactInformation);
        }
    }

   


}
