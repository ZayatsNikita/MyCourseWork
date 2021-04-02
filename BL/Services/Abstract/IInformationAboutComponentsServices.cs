using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BL.dtoModels;


namespace BL.Services.Abstract
{
    public interface IInformationAboutComponentsServices
    {
        public void Create(InformationAboutComponents info);
        public List<InformationAboutComponents> Read(
            int minId,
            int maxId,

            int minComponetId,
            int maxComponetId,

            int minCountOfComponents,
            int maxCountOfComponents
            
            );
        public void Delete(InformationAboutComponents info);
        public void Update(InformationAboutComponents info, int componetId, int countOfComponents);
    }
}