using System;
using System.Collections.Generic;
using System.Text;
using BL.DtoModels;
using System.Linq;
using DL.Repositories.Abstract;
using BL.Exceptions;
using BL.Services.Abstract.ValidationInterfaces;

namespace BL.Services.Validaton
{
    public class WorkerValidationService : IWorkerValidator
    {
        private IWorkerEntityRepo _workerEntityRepo;

        public WorkerValidationService(IWorkerEntityRepo workerEntityRepo)
        {
            _workerEntityRepo = workerEntityRepo;
        }

        public void CheckForValidyToCreate(Worker worker)
        {
            _ = worker ?? throw new ValidationException("Worker is null.");

            var workersNumbers = _workerEntityRepo.Read().Select(x => x.PassportNumber);

            if (workersNumbers.Any(x => x == (worker?.PassportNumber ?? 0)))
            {
                throw new ValidationException(Messages.ExsistingWorkerId);
            }

            if ((worker?.PersonalData?.Length ?? 0) < 3 || worker.PersonalData.Length > 100)
            {
                throw new ValidationException(Messages.WrongFIOLength);
            }
        }

        public void CheckForValidyToUpdate(Worker worker)
        {
            if ((worker?.PersonalData?.Length ?? 0) < 3 || worker.PersonalData.Length > 100)
            {
                throw new ValidationException(Messages.WrongFIOLength);
            }
        }
    }
}
