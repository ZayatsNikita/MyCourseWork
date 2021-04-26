﻿using AutoMapper;
using PL.Models;
using System.Collections.Generic;

namespace PL.Infrastructure.Services
{
    public class ClientServices : Abstract.IClientServices
    {
        private BL.Services.Abstract.IClientServices _repository;
        private Mapper _mapper;
        public ClientServices(BL.Services.Abstract.IClientServices repository, Mapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public void Create(Client client)
        {
            _repository.Create(_mapper.Map<Client, BL.DtoModels.Client>(client));
        }

        public void Delete(Client client)
        {
            _repository.Delete(_mapper.Map<Client, BL.DtoModels.Client>(client));
        }

        public List<Client> Read(int MinId = -1, int MaxId = -1, string title = null, string contactInformation = null)
        {
            List<Client> result = _mapper.Map<List<BL.DtoModels.Client>, List<Client>>(_repository.Read(MinId, MaxId, title, contactInformation));
            return result;
        }

        public void Update(Client client, string title = null, string contactInformation = null)
        {
            _repository.Update(_mapper.Map<Client, BL.DtoModels.Client>(client), title, contactInformation);
        }
    }
}
