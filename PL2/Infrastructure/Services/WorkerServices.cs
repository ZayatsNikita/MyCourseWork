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
            _servises.Create(_mapper.Map<Worker, BL.dtoModels.Worker>(worker));
        }

        public void Delete(Worker worker)
        {
            _servises.Delete(_mapper.Map<Worker, BL.dtoModels.Worker>(worker));
        }

        public List<Worker> Read(int minPassportNumber, int maxPassportNumber, string PersonalData)
        {
            List<Worker>  result = _mapper.Map<List<BL.dtoModels.Worker>, List<Worker>>(_servises.Read(minPassportNumber, maxPassportNumber , PersonalData));
            return result;
        }

        public void Update(Worker worker, string PersonalData)
        {
            _servises.Update(_mapper.Map<Worker, BL.dtoModels.Worker>(worker), PersonalData);
        }
    }

   


}
