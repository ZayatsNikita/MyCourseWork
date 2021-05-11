using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DL.Entities;

namespace DL.Repositories.Abstract
{
    public interface IServiceEntityRepository
    {
        public void Create(ServiceEntity service);
        public List<ServiceEntity> Read(
            int MinId =Repository.DefValInt,
            int MaxId = Repository.DefValInt,
            string Title = null,
            string Description = null,
            decimal maxPrice = Repository.DefValDec,
            decimal minPrice = Repository.DefValDec
            );
        public void Delete(ServiceEntity service);
        public void Update(ServiceEntity service, string title = null, string Description= null, decimal Price = Repository.DefValDec);
    }
}