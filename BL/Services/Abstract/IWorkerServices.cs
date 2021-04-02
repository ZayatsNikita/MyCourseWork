using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BL.dtoModels;

namespace BL.Services.Abstract
{
    public interface IWorkerServices
    {
        public void Create(Worker worker);
        public List<Worker> Read(
            int minPassportNumber,
            int maxPassportNumber,
            string PersonalData
            );
        public void Delete(Worker worker);
        public void Update(Worker worker, string PersonalData);
    }
}