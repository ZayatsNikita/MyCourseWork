using PL.Models;
using System.Collections.Generic;

namespace PL.Infrastructure.Services.Abstract
{
    public interface IServiceServices
    {

        public void Create(Service service);
        public List<Service> Read(
            int MinId = -1,
            int MaxId = -1,
            string Title = null,
            string Description = null,
            decimal maxPrice = -1,
            decimal minPrice = -1
            );
        public void Delete(Service service);
        public void Update(Service service);

    }
}
