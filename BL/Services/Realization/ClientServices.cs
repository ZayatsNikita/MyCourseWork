using AutoMapper;
using BL.DtoModels;
using DL.Entities;
using DL.Repositories.Abstract;
using System.Collections.Generic;
using BL.Services.Validaton;
using BL.Services.Abstract.ValidationInterfaces;

namespace BL.Services
{
    public class ClientServices : Abstract.IClientServices
    {
        private IClientEntiryRepo _repository;
        
        private Mapper _mapper;

        private IClientValidator _clientValidator;

        public ClientServices(IClientEntiryRepo repository, Mapper mapper, IClientValidator clientValidator)  
        {
            _mapper = mapper;

            _repository = repository;

            _clientValidator = clientValidator;
        }
        
        public int Create(Client client)
        {
            _clientValidator.CheckForValidity(client);

            var id = _repository.Create(_mapper.Map<Client, ClientEntity>(client));

            return id;
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public List<Client> Read()
        {
            List<Client> result = _mapper.Map<List<ClientEntity>, List<Client>>(_repository.Read());
            return result;
        }

        public Client ReadById(int id) => _mapper.Map<Client>(_repository.ReadById(id));

        public void Update(Client client)
        {
            _clientValidator.CheckForValidity(client);

            _repository.Update(_mapper.Map<Client, ClientEntity>(client));
        }
    }
}
