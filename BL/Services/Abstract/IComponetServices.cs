using BL.DtoModels;
using System.Collections.Generic;

namespace BL.Services.Abstract
{
    public interface IComponetServices
    {
        public void Create(Componet componet);
        public List<Componet> Read(
            int minId = -1,
            int maxId = -1,
            string title = null,
            decimal maxPrice = -1,
            decimal minPrice = -1
            );
        public void Delete(Componet componet);
        public void Update(Componet componet, string title, decimal Price);
    }
}