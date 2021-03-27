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
        public object Create(ClientEntity client);
        public List<ClientEntity> Read();
        public void Delete(ClientEntity clientEntity);
        public void Update(ClientEntity clientEntity);
    }
}