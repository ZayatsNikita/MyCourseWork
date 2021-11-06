using BL.DtoModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Services.Abstract.ValidationInterfaces
{
    public interface IWorkerValidator
    {
        void CheckForValidyToCreate(Worker worker);

        void CheckForValidyToUpdate(Worker worker);
    }
}
