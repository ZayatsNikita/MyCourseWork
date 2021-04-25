using System;
using System.Collections.Generic;
using System.Text;
using DL.Repositories.Abstract;
using BL.DtoModels;
using BL.Mappers;
using AutoMapper;
using DL.Entities;

namespace BL.Services
{
    public class WorkerServices : Abstract.IWorkerServices
    {
        private IWorkerEntityRepo _repository;
        private Mapper _mapper;
        public WorkerServices(IWorkerEntityRepo repository, Mapper mapper)  
        {
            _mapper = mapper;
            _repository = repository; 
        }

        public void Create(Worker worker)
        {
            _repository.Create(_mapper.Map<Worker, WorkerEntity>(worker));
        }

        public void Delete(Worker worker)
        {
            _repository.Delete(_mapper.Map<Worker, WorkerEntity>(worker));
        }

        public List<Worker> Read(int minPassportNumber, int maxPassportNumber, string PersonalData)
        {
            List<Worker>  result = _mapper.Map<List<WorkerEntity>, List<Worker>>(_repository.Read(minPassportNumber, maxPassportNumber , PersonalData));
            return result;
        }

        public void Update(Worker worker, string PersonalData)
        {
            _repository.Update(_mapper.Map<Worker, WorkerEntity>(worker), PersonalData);
        }
    }

   


}
