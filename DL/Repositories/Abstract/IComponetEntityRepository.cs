using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DL.Entities;

namespace DL.Repositories.Abstract
{
    public interface IComponetEntityRepository
    {
        public void Create(ComponetEntity componet);
        public List<ComponetEntity> Read(
            int minId,
            int maxId,
            string title,
            decimal maxPrice,
            decimal minPrice
            );
        public void Delete(ComponetEntity componet);
        public void Update(ComponetEntity componet, string title, decimal Price);
    }
}