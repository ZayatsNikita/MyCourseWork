﻿using AutoMapper;
using PL.Models;
using System.Collections.Generic;


namespace PL.Infrastructure.Services
{
    public class BuildStandartService : Abstract.IBuildStandartService
    {
        private BL.Services.Abstract.IBuildStandartServices _repository;
        private Mapper _mapper;
        public BuildStandartService(BL.Services.Abstract.IBuildStandartServices repository, Mapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public void Create(BuildStandart сomponetService)
        {
            _repository.Create(_mapper.Map<BuildStandart, BL.DtoModels.Combined.BuildStandart
                >(сomponetService));
        }

        public void Delete(BuildStandart сomponetService)
        {
            _repository.Delete(_mapper.Map<BuildStandart, BL.DtoModels.Combined.BuildStandart>(сomponetService));
        }

        public List<BuildStandart> Read(int minId = Constans.DefIntVal, int maxId = Constans.DefIntVal, int minServiceId = Constans.DefIntVal, int maxServiceId = Constans.DefIntVal, int minComponetId = Constans.DefIntVal, int maxComponetId = Constans.DefIntVal)
        {
            List<BuildStandart> result = _mapper.Map<List<BL.DtoModels.Combined.BuildStandart>, List<BuildStandart>>(_repository.Read(minId, maxId, minServiceId, maxServiceId, minComponetId, maxComponetId));
            return result;
        }

        public void Update(BuildStandart сomponetService, int serviceId = -1, int componetId = -1)
        {
            _repository.Update(_mapper.Map<BuildStandart, BL.DtoModels.Combined.BuildStandart>(сomponetService), serviceId, componetId);
        }
    }
}
