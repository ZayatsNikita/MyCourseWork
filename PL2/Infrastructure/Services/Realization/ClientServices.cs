using AutoMapper;
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
            _repository.Delete(client.Id);
        }

        public List<Client> Read()
        {
            List<Client> result = _mapper.Map<List<BL.DtoModels.Client>, List<Client>>(_repository.Read());
            return result;
        }

        public Client ReadById(int id)
        {
            return _mapper.Map<BL.DtoModels.Client, Client>(_repository.ReadById(id));
        }

        public void Update(Client client)
        {
            _repository.Update(_mapper.Map<Client, BL.DtoModels.Client>(client));
        }
    }
}
