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
            int MinId,
            int MaxId,
            int minInfoAboutComponentId,
            int maxInfoAboutComponentId,
            string Title,
            string Description,
            decimal maxPrice,
            decimal minPrice
            );
        public void Delete(ServiceEntity service);
        public void Update(ServiceEntity service,int infoAboutComponentId, string title, string Description, decimal Price);
    }
}