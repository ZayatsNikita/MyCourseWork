using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BL.DtoModels;

namespace BL.Services.Abstract
{
    public interface IWorkerServices
    {
        public void Create(Worker worker);
        public List<Worker> Read(
            int minPassportNumber = Constants.DefIntVal,
            int maxPassportNumber = Constants.DefIntVal, 
            string PersonalData = null
            );
        public void Delete(Worker worker);
        public void Update(Worker worker, string PersonalData);
    }
}