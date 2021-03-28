using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DL.Entities;

namespace DL.Repositories.Abstract
{
    public interface IClientEntiryRepo
    {
        public void Create(ClientEntity client);
        public List<ClientEntity> Read(
            int MinId,
            int MaxId,
            string Title,
            string ContactInformation
            );
        public void Delete(ClientEntity clientEntity);
        public void Update(ClientEntity clientEntity, string title, string contactInformation);
    }
}