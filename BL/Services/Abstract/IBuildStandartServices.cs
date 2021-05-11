using System;
using System.Collections.Generic;
using System.Text;
using BL.DtoModels.Combined;
using BL.Services;

namespace BL.Services.Abstract
{
    public interface IBuildStandartServices
    {
        public void Create(BuildStandart buildStandart);
        public List<BuildStandart> Read(
            int minId = Constants.DefIntVal,
            int maxId = Constants.DefIntVal,
            int minServiceId = Constants.DefIntVal,
            int maxServiceId = Constants.DefIntVal,
            int minComponetId = Constants.DefIntVal,
            int maxComponetId = Constants.DefIntVal
            );
        public void Delete(BuildStandart buildStandart);
        public void Update(BuildStandart buildStandart, int serviceId = Constants.DefIntVal, int componetId = Constants.DefIntVal);
    }
}
