using AutoMapper;
using PL.Models;
using System.Collections.Generic;


namespace PL.Infrastructure.Services
{
    public class BuildStandartService : Abstract.IBuildStandartService
    {
        private BL.Services.Abstract.IServiceComponentsService _serviceComponentService;
        
        private Mapper _mapper;
        
        public BuildStandartService(BL.Services.Abstract.IServiceComponentsService repository, Mapper mapper)
        {
            _mapper = mapper;
            _serviceComponentService = repository;
        }

        public void Create(BuildStandart сomponetService)
        {
            _serviceComponentService.Create(_mapper.Map<BuildStandart, BL.DtoModels.Combined.FullServiceComponents
                >(сomponetService));
        }

        public void Delete(BuildStandart сomponetService)
        {
            _serviceComponentService.Delete(сomponetService.Id);
        }

        public List<BuildStandart> Read()
        {
            List<BuildStandart> result = _mapper.Map<List<BL.DtoModels.Combined.FullServiceComponents>, List<BuildStandart>>(_serviceComponentService.Read());

            return result;
        }

        public BuildStandart ReadById(int id)
        {
            return _mapper.Map<BL.DtoModels.Combined.FullServiceComponents, BuildStandart>(_serviceComponentService.ReadById(id));
        }

        public void Update(BuildStandart сomponetService)
        {
            _serviceComponentService.Update(_mapper.Map<BuildStandart, BL.DtoModels.Combined.FullServiceComponents>(сomponetService));
        }
    }
}
