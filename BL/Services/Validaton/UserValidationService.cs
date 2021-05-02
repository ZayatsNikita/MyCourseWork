using BL.DtoModels;
using BL.Exceptions;
using System.Collections.Generic;
using System.Linq;
namespace BL.Services.Validaton
{
    public static class UserValidationService
    {
        public static bool IsValid(this User user, List<string> logins)
        {
            if ((user?.Login?.Length ?? 0) < 3 || user.Login.Length > 25)
            {
                throw new ValidationException(Messages.WrongLoginLength);
            }
            if ((user?.Password?.Length ?? 0) < 3 || user.Password.Length > 25)
            {
                throw new ValidationException(Messages.WrongPasswordLength);
            }
            if (logins.Any(x => x == user.Login))
            {
                throw new ValidationException(Messages.ExsistingLogin);
            }
            return true;
        }
    }
}
