using System;
using System.Collections.Generic;
using System.Text;
using BL.DtoModels.Combined;
namespace BL.Services.Abstract
{
    interface IOperationServices
    {
        public List<BuildStandart> Read(int componentId = Constants.DefIntVal, int serviceId = Constants.DefIntVal);
        public void Create(BuildStandart operation);
        public void Delete(BuildStandart operation);
        public void Update(BuildStandart operation);
    }
}
