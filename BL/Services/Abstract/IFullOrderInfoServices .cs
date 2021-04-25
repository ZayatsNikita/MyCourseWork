using BL.DtoModels.Combined;
using System.Collections.Generic;

namespace BL.Services.Abstract
{
    public interface IFullOrderInfoServices
    {
        public void Create(FullOrderInfo orderInfo);
        public List<FullOrderInfo> Read(
            int minId = -1,
            int maxId = -1,
            int minCountOfServicesRendered = -1,
            int maxCountOfServicesRendered = -1,
            int minBuildStandartId = -1,
            int maxBuildStandartId = -1, 
            int minOrderNumber = -1,
            int maxOrderNumber = -1
            );
        public void Delete(FullOrderInfo orderInfo);
        public void Update(FullOrderInfo orderInfo,
            int OrderNumber,
            int CountOfServicesRendered,
            int BuildStandartId
            );
    }
}