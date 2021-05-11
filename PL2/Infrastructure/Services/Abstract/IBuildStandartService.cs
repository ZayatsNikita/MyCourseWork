using PL.Models;
using System.Collections.Generic;

namespace PL.Infrastructure.Services.Abstract
{
    public interface IBuildStandartService
    {
        public void Create(BuildStandart сomponetService);
        public List<BuildStandart> Read(
            int minId = Constans.DefIntVal,
            int maxId = Constans.DefIntVal,
            int minServiceId = Constans.DefIntVal,
            int maxServiceId = Constans.DefIntVal,
            int minComponetId = Constans.DefIntVal,
            int maxComponetId = Constans.DefIntVal
            );
        public void Delete(BuildStandart сomponetService);
        public void Update(BuildStandart сomponetService, int serviceId = Constans.DefIntVal, int componetId = Constans.DefIntVal);
    }
}
