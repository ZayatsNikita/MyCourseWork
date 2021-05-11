using BL.DtoModels;
using System.Collections.Generic;

namespace BL.Services.Abstract
{
    public interface IServiceServices
    {
        public void Create(Service service);
        public List<Service> Read(
            int MinId = Constants.DefIntVal,
            int MaxId = Constants.DefIntVal,
            string Title = null,
            string Description = null,
            decimal maxPrice = Constants.DefIntVal,
            decimal minPrice = Constants.DefIntVal
            );
        public void Delete(Service service);
        public void Update(Service service, string titl=null, string Description = null, decimal Price = Constants.DefDecVal);
    }
}