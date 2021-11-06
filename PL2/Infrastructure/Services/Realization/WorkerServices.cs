using AutoMapper;
using PL.Models;
using System.Collections.Generic;

namespace PL.Infrastructure.Services
{
    public class WorkerServices : Abstract.IWorkerServices
    {
        private BL.Services.Abstract.IWorkerServices _servises;
        
        private Mapper _mapper;

        public WorkerServices(BL.Services.Abstract.IWorkerServices servises, Mapper mapper)  
        {
            _mapper = mapper;
            _servises = servises; 
        }

        public void Create(Worker worker)
        {
            _servises.Create(_mapper.Map<Worker, BL.DtoModels.Worker>(worker));
        }

        public void Delete(Worker worker)
        {
            _servises.Delete(worker.PassportNumber);
        }

        public List<Worker> Read()
        {
            List<Worker>  result = _mapper.Map<List<BL.DtoModels.Worker>, List<Worker>>(_servises.Read());
            
            return result;
        }

        public void Update(Worker worker)
        {
            _servises.Update(_mapper.Map<Worker, BL.DtoModels.Worker>(worker));
        }
    }

   


}
