using PL.Models;
using System.Collections.Generic;

namespace PL.Infrastructure.Services.Abstract
{
    public interface IComponentServices
    {
        public void Create(Componet componet);
        public List<Componet> Read(
            int minId = Constans.DefIntVal,
            int maxId = Constans.DefIntVal,
            string title = null,
            string productionStandards = null,
            decimal maxPrice = Constans.DefIntVal,
            decimal minPrice = Constans.DefIntVal
            );
        public void Delete(Componet componet);
        public void Update(Componet componet);
    }
}
