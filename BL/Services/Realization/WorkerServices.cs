using AutoMapper;
using BL.DtoModels;
using BL.Services.Abstract.ValidationInterfaces;
using DL.Entities;
using DL.Repositories.Abstract;
using System.Collections.Generic;

namespace BL.Services
{
    public class WorkerServices : Abstract.IWorkerServices
    {
        private IWorkerEntityRepo _repository;
        
        private Mapper _mapper;

        private IWorkerValidator _workerValidator;


        public WorkerServices(IWorkerEntityRepo repository, Mapper mapper, IWorkerValidator workerValidator)  
        {
            _workerValidator = workerValidator;

            _mapper = mapper;

            _repository = repository; 
        }

        public int Create(Worker worker)
        {
            _workerValidator.CheckForValidyToCreate(worker);

            var id = _repository.Create(_mapper.Map<Worker, WorkerEntity>(worker));

            return id;
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public List<Worker> Read() => _mapper.Map<List<WorkerEntity>, List<Worker>>(_repository.Read());

        public Worker ReadById(int id) => _mapper.Map<WorkerEntity, Worker>(_repository.ReadById(id));

        public void Update(Worker worker)
        {
            _workerValidator.CheckForValidyToUpdate(worker);

            _repository.Update(_mapper.Map<Worker, WorkerEntity>(worker));
        }
    }
}
