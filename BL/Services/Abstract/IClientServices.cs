using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BL.dtoModels;

namespace BL.Services.Abstract
{
    public interface IClientServices
    {
        public void Create(Client client);
        public List<Client> Read(
            int MinId,
            int MaxId,
            string Title,
            string ContactInformation
            );
        public void Delete(Client clientEntity);
        public void Update(Client clientEntity, string title, string contactInformation);
    }
}