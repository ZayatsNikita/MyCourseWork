using System;
using System.Collections.Generic;
using System.Text;
using BL.DtoModels;
using System.Linq;
using BL.Exceptions;
namespace BL.Services.Validaton
{
    public static class WorkerValidationService
    {
        public static bool IsValid(this Worker worker, List<int> codesOfWorkers)
        {
            if (codesOfWorkers.Any(x => x == (worker?.PassportNumber ?? 0)))
            {
                throw new ValidationException(Messages.ExsistingWorkerId);
            }
            if ((worker?.PersonalData?.Length ?? 0) < 3 || worker.PersonalData.Length > 100)
            {
                throw new ValidationException(Messages.WrongFIOLength);
            }
            return true;
        }
    }
}
