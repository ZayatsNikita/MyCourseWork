using System;
using System.Collections.Generic;
using System.Text;
using BL.DtoModels.Combined;

namespace BL.Services.Abstract
{
    public interface IBuildStandartServices
    {
        public void Create(BuildStandart buildStandart);
        public List<BuildStandart> Read(
            int minId = -1,
            int maxId = -1,
            int minServiceId = -1,
            int maxServiceId = -1,
            int minComponetId = -1,
            int maxComponetId = -1
            );
        public void Delete(BuildStandart buildStandart);
        public void Update(BuildStandart buildStandart, int serviceId = -1, int componetId = -1);
    }
}
