using PL.Models;
using System.Collections.Generic;

namespace PL.Infrastructure.Services.Abstract
{
    public interface IBuildStandartService
    {
        public void Create(BuildStandart сomponetService);
        public List<BuildStandart> Read();
        public BuildStandart ReadById(int id);
        public void Delete(BuildStandart сomponetService);
        public void Update(BuildStandart сomponetService);
    }
}
