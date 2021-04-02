using AutoMapper;
using BL.dtoModels;
using DL.Entities;
using DL.Repositories.Abstract;
using System.Collections.Generic;

namespace BL.Services
{
    public class InformationAboutComponentsServices : Abstract.IInformationAboutComponentsServices
    {
        private IInformationAboutComponentsEntityRepository _repository;
        private Mapper _mapper;
        public InformationAboutComponentsServices(IInformationAboutComponentsEntityRepository repository, Mapper mapper)  
        {
            _mapper = mapper;
            _repository = repository;
        }

        public void Create(InformationAboutComponents info)
        {
            _repository.Create(_mapper.Map<InformationAboutComponents, InformationAboutComponentsEntity>(info));
        }

        public void Delete(InformationAboutComponents info)
        {
            _repository.Delete(_mapper.Map<InformationAboutComponents, InformationAboutComponentsEntity>(info));
        }

        public List<InformationAboutComponents> Read(int minId, int maxId, int minComponetId, int maxComponetId, int minCountOfComponents, int maxCountOfComponents)
        {
            List<InformationAboutComponents> result = _mapper.Map<List<InformationAboutComponentsEntity>, List<InformationAboutComponents>>(_repository.Read(minId, maxId, minComponetId, maxComponetId, minCountOfComponents, maxCountOfComponents));
            return result;
        }

        public void Update(InformationAboutComponents info, int componetId, int countOfComponents)
        {
            _repository.Update(_mapper.Map<InformationAboutComponents, InformationAboutComponentsEntity>(info), componetId, countOfComponents);
        }
    }

   


}
