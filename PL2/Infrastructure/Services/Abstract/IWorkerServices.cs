using PL.Models;
using System.Collections.Generic;

namespace PL.Infrastructure.Services.Abstract
{
    public interface IWorkerServices
    {
        public void Create(Worker worker);
        public List<Worker> Read();
        public void Delete(Worker worker);
        public void Update(Worker worker);
    }
}