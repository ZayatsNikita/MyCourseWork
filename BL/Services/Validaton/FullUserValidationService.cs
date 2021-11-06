using BL.DtoModels.Combined;
using BL.Exceptions;
using BL.Services.Abstract.ValidationInterfaces;

namespace BL.Services.Validaton
{
    public class FullUserValidationService : IFullUserValidator
    {
        private readonly IWorkerValidator _workerValidator;

        private readonly IUserValidator _userValidator;

        public FullUserValidationService(IWorkerValidator workerValidator, IUserValidator userValidator)
        {
            _userValidator = userValidator;

            _workerValidator = workerValidator;
        }

        public void CheckForValidyToCreate(FullUser user)
        {
            _workerValidator.CheckForValidyToCreate(user.Worker);

            if (user.User != null)
            {
                if ((user?.Roles?.Count ?? 0) == 0)
                {
                    throw new ValidationException(Messages.NoRoleSelected);
                }

                _userValidator.CheckForValidity(user.User);
            }
        }

        public void CkeckForValidyToUpdate(FullUser user)
        {
            _workerValidator.CheckForValidyToUpdate(user.Worker);

            if (user.User != null)
            {
                if ((user?.Roles?.Count ?? 0) == 0)
                {
                    throw new ValidationException(Messages.NoRoleSelected);
                }

                _userValidator.CheckForValidity(user.User);
            }
        }
    }
}
