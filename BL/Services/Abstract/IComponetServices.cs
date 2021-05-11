using BL.DtoModels;
using System.Collections.Generic;

namespace BL.Services.Abstract
{
    public interface IComponetServices
    {
        public void Create(Componet componet);
        public List<Componet> Read(
            int minId = Constants.DefIntVal,
            int maxId = Constants.DefIntVal,
            string title = null,
            string standartParam = null,
            decimal maxPrice = Constants.DefIntVal,
            decimal minPrice = Constants.DefIntVal
            );
        public void Delete(Componet componet);
        public void Update(Componet componet, string title=null, string standartParam=null, decimal Price= Constants.DefIntVal);
    }
}