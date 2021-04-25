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
            int minId = -1,
            int maxId = -1,
            string title = null,
            decimal maxPrice = -1,
            decimal minPrice = -1);
        public void Delete(ComponetEntity componet);
        public void Update(ComponetEntity componet, string title, decimal Price);
    }
}