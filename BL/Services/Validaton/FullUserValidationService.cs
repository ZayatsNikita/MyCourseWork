using System;
using System.Collections.Generic;
using System.Text;
using BL.DtoModels.Combined;
using System.Linq;
using BL.Exceptions;
namespace BL.Services.Validaton
{
    public static class FullUserValidationService
    {
        private static bool IsValid(this FullUser user, List<int> codesOfWorkers, List<string> logins)
        {
            if(codesOfWorkers.Any(x=>x == user.Worker.PassportNumber))
            {
                throw new ValidationException(Messages.ExsistingWorkerId);
            }
            if((user.Worker?.PersonalData?.Length ?? 0 ) < 3  || user.Worker.PersonalData.Length > 100)
            {
                throw new ValidationException(Messages.WrongFIOLength);
            }
            if (user.User != null)
            {
                if((user?.User?.Login?.Length ?? 0) < 3 || user.User.Login.Length > 25)
                {
                    throw new ValidationException(Messages.WrongLoginLength);
                }
                if ((user?.User?.Password?.Length ?? 0) < 3 || user.User.Password.Length > 25)
                {
                    throw new ValidationException(Messages.WrongPasswordLength);
                }
                if(logins.Any(x=>x == user.User.Login))
                {
                    throw new ValidationException(Messages.ExsistingLogin);
                }
                if (user.Roles.Count == 0)
                {
                    throw new ValidationException(Messages.NoRoleSelected);
                }
                return true;
            }
            throw new ValidationException(Messages.ObjectNotCreatedMessage);
        }

        public static bool IsValid(this FullUser user)
        {
            if (user.Roles!= null && user.Roles.Count == 0)
            {
                throw new ValidationException(Messages.NoRoleSelected);
            }
            return true;
        }
    }
}
