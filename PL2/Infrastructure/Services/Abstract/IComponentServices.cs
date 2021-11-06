using PL.Models;
using System.Collections.Generic;

namespace PL.Infrastructure.Services.Abstract
{
    public interface IComponentServices
    {
        public void Create(Componet componet);
        public List<Componet> Read();
        public void Delete(Componet componet);
        public void Update(Componet componet);
        public Componet ReadById(int id);
    }
}
