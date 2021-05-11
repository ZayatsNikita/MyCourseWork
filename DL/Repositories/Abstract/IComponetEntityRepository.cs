using System.Collections.Generic;
using DL.Entities;

namespace DL.Repositories.Abstract
{
    public interface IComponetEntityRepository
    {
        public void Create(ComponetEntity componet);
        public List<ComponetEntity> Read(
            int minId = Repository.DefValInt,
            int maxId = Repository.DefValInt,
            string title = null,
            string productionStandards = null,
            decimal maxPrice = Repository.DefValDec,
            decimal minPrice = Repository.DefValDec);
        public void Delete(ComponetEntity componet);
        public void Update(ComponetEntity componet, string title = null,string productionStandards=null, decimal Price = Repository.DefValDec);
    }
}