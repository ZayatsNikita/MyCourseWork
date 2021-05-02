using BL.DtoModels;
using BL.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace BL.Services.Validaton
{
    public static class ClientValidationService
    {
        public static bool IsValid(this Client client)
        {
            if ((client?.ContactInformation?.Length ?? 0) < 3 || client.ContactInformation.Length > 100)
            {
                throw new ValidationException(Messages.WrongContactInfoLength);
            }
            if ((client?.Title?.Length ?? 0) < 3 || client.Title.Length > 50)
            {
                throw new ValidationException(Messages.WrongClientTitleInfoLength);
            }
            return true;
        }
    }
}
