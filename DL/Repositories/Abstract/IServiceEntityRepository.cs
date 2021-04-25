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
            int MinId =-1,
            int MaxId = -1,
            string Title = null,
            string Description = null,
            decimal maxPrice = -1,
            decimal minPrice = -1
            );
        public void Delete(ServiceEntity service);
        public void Update(ServiceEntity service, string title = null, string Description= null, decimal Price = -1);
    }
}