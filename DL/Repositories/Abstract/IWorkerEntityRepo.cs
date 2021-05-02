using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DL.Entities;

namespace DL.Repositories.Abstract
{
    public interface IWorkerEntityRepo
    {
        public void Create(WorkerEntity worker);
        public List<WorkerEntity> Read(
            int minPassportNumber =-1,
            int maxPassportNumber = -1,
            string PersonalData = null
            );
        public void Delete(WorkerEntity clientEntity);
        public void Update(WorkerEntity clientEntity, string PersonalData);
    }
}