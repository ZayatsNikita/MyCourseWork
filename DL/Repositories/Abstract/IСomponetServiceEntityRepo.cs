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
            int minId =- 1,
            int maxId = -1,
            int minServiceId = -1,
            int maxServiceId = -1,
            int minComponetId = -1,
            int maxComponetId = -1
            );
        public void Delete(СomponetServiceEntity сomponetServiceEntity);
        public void Update(СomponetServiceEntity сomponetServiceEntity, int serviceId = -1, int componetId = -1);
    }
}
