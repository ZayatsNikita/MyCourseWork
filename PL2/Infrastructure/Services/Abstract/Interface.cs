using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PL.Models;
namespace PL.Infrastructure.Services.Abstract
{
    public interface IClientServices
    {
        public void Create(Client client);
        public List<Client> Read(
            int MinId = Constans.DefIntVal,
            int MaxId = Constans.DefIntVal,
            string Title = null,
            string ContactInformation = null
            );
        public void Delete(Client clientEntity);
        public void Update(Client clientEntity);
    }
}
