using PL.Models;
using System.Collections.Generic;

namespace PL.Infrastructure.Services.Abstract
{
    public interface IServiceServices
    {

        public void Create(Service service);
        public List<Service> Read(
            int MinId = Constans.DefIntVal,
            int MaxId = Constans.DefIntVal,
            string Title = null,
            string Description = null,
            decimal maxPrice = Constans.DefIntVal,
            decimal minPrice = Constans.DefIntVal
            );
        public void Delete(Service service);
        public void Update(Service service);

    }
}
