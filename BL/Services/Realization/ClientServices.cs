using AutoMapper;
using BL.DtoModels;
using DL.Entities;
using DL.Repositories.Abstract;
using System.Collections.Generic;
using BL.Services.Validaton;

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
            if (client.IsValid())
            {
                _repository.Create(_mapper.Map<Client, ClientEntity>(client));
            }
        }

        public void Delete(Client client)
        {
            _repository.Delete(_mapper.Map<Client, ClientEntity>(client));
        }

        public List<Client> Read(int MinId= Constants.DefIntVal, int MaxId= Constants.DefIntVal, string title=null, string contactInformation = null)
        {
            List<Client> result = _mapper.Map<List<ClientEntity>, List<Client>>(_repository.Read(MinId, MaxId ,title, contactInformation));
            return result;
        }

        public void Update(Client client, string title = null, string contactInformation = null)
        {
            if (client.IsValid())
            {
                _repository.Update(_mapper.Map<Client, ClientEntity>(client), title, contactInformation);
            }
        }
    }
}
