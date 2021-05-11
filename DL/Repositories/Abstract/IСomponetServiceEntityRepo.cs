using System;
using System.Collections.Generic;
using System.Text;
using DL.Entities;
namespace DL.Repositories.Abstract
{
    public interface IСomponetServiceEntityRepo
    {
        public void Create(СomponetServiceEntity сomponetServiceEntity);
        public List<СomponetServiceEntity> Read(
            int minId = Repository.DefValInt,
            int maxId = Repository.DefValInt,
            int minServiceId = Repository.DefValInt,
            int maxServiceId = Repository.DefValInt,
            int minComponetId = Repository.DefValInt,
            int maxComponetId = Repository.DefValInt
            );
        public void Delete(СomponetServiceEntity сomponetServiceEntity);
        public void Update(СomponetServiceEntity сomponetServiceEntity, int serviceId = Repository.DefValInt, int componetId = Repository.DefValInt);
    }
}
