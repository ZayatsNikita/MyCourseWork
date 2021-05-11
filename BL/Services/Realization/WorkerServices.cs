using AutoMapper;
using BL.DtoModels;
using BL.Services.Validaton;
using DL.Entities;
using DL.Repositories.Abstract;
using System.Collections.Generic;
using System.Linq;

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
            if (worker.IsValid(_repository.Read().Select(x => x.PassportNumber).ToList()))
            {
                _repository.Create(_mapper.Map<Worker, WorkerEntity>(worker));
            }
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
            if (worker.IsValid(_repository.Read().Select(x => x.PassportNumber).Where(x=>x!=worker.PassportNumber).ToList()))
            {
                _repository.Update(_mapper.Map<Worker, WorkerEntity>(worker), PersonalData);
            }
        }
    }
}
