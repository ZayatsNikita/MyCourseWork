using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BL.dtoModels;

namespace BL.Services.Abstract
{
    public interface IServiceServices
    {
        public void Create(Service service);
        public List<Service> Read(
            int MinId,
            int MaxId,
            int minInfoAboutComponentId,
            int maxInfoAboutComponentId,
            string Title,
            string Description,
            decimal maxPrice,
            decimal minPrice
            );
        public void Delete(Service service);
        public void Update(Service service,int infoAboutComponentId, string title, string Description, decimal Price);
    }
}