using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BL.dtoModels;

namespace BL.Services.Abstract
{
    public interface IComponetServices
    {
        public void Create(Componet componet);
        public List<Componet> Read(
            int minId,
            int maxId,
            string title,
            decimal maxPrice,
            decimal minPrice
            );
        public void Delete(Componet componet);
        public void Update(Componet componet, string title, decimal Price);
    }
}