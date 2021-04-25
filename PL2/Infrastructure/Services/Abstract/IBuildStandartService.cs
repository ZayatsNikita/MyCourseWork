using PL.Models;
using System.Collections.Generic;

namespace PL.Infrastructure.Services.Abstract
{
    public interface IBuildStandartService
    {
        public void Create(BuildStandart сomponetService);
        public List<BuildStandart> Read(
            int minId = -1,
            int maxId = -1,
            int minServiceId = -1,
            int maxServiceId = -1,
            int minComponetId = -1,
            int maxComponetId = -1
            );
        public void Delete(BuildStandart сomponetService);
        public void Update(BuildStandart сomponetService, int serviceId = -1, int componetId = -1);
    }
}
