using PL.Models;
using System.Collections.Generic;

namespace PL.Infrastructure.Services.Abstract
{
    public interface IServiceServices
    {

        public void Create(Service service);
        public List<Service> Read();
        public void Delete(Service service);
        public void Update(Service service);
        public Service ReadById(int id);

    }
}
