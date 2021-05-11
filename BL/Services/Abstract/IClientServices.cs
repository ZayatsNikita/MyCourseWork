using BL.DtoModels;
using System.Collections.Generic;

namespace BL.Services.Abstract
{
    public interface IClientServices
    {
        public void Create(Client client);
        public List<Client> Read(
            int MinId = Constants.DefIntVal,
            int MaxId = Constants.DefIntVal,
            string Title = null,
            string ContactInformation = null
            );
        public void Delete(Client clientEntity);
        public void Update(Client clientEntity, string title, string contactInformation);
    }
}