using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DL.Entities;

namespace DL.Repositories.Abstract
{
    public interface IInformationAboutComponentsEntityRepository
    {
        public void Create(InformationAboutComponentsEntity info);
        public List<InformationAboutComponentsEntity> Read(
            int minId,
            int maxId,

            int minComponetId,
            int maxComponetId,

            int minCountOfComponents,
            int maxCountOfComponents
            
            );
        public void Delete(InformationAboutComponentsEntity info);
        public void Update(InformationAboutComponentsEntity info, int componetId, int countOfComponents);
    }
}