using BL.DtoModels;
using BL.Exceptions;
using System.Collections.Generic;
using System.Linq;
using BL.Services.Abstract.ValidationInterfaces;
using DL.Repositories.Abstract;

namespace BL.Services.Validaton
{
    public class UserValidationService : IUserValidator
    {
        private IUserEntityRepo _userEntityRepo;

        public UserValidationService(IUserEntityRepo userEntityRepo)
        {
            _userEntityRepo = userEntityRepo;
        }

        public void CheckForValidity(User user)
        {
            _ = user ?? throw new ValidationException("User is null.");

            if ((user?.Login?.Length ?? 0) < 3 || user.Login.Length > 25)
            {
                throw new ValidationException(Messages.WrongLoginLength);
            }
            if ((user?.Password?.Length ?? 0) < 3 || user.Password.Length > 25)
            {
                throw new ValidationException(Messages.WrongPasswordLength);
            }

            var logins = _userEntityRepo.Read()
                .Where(x => x.Id != user.Id)
                .Select(x => x.Login);

            if (logins.Any(x => string.Compare(x, user.Login, true) == 0))
            {
                throw new ValidationException(Messages.ExsistingLogin);
            }
        }
    }
}
