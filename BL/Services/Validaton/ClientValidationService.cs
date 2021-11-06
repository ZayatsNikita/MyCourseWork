using BL.DtoModels;
using BL.Exceptions;
using BL.Services.Abstract.ValidationInterfaces;

namespace BL.Services.Validaton
{
    public class ClientValidationService : IClientValidator
    {
        public void CheckForValidity(Client client)
        {
            _ = client ?? throw new ValidationException("Client is null.");

            if ((client?.ContactInformation?.Length ?? 0) < 3 || client.ContactInformation.Length > 100)
            {
                throw new ValidationException(Messages.WrongContactInfoLength);
            }
            if ((client?.Title?.Length ?? 0) < 3 || client.Title.Length > 50)
            {
                throw new ValidationException(Messages.WrongClientTitleInfoLength);
            }
        }
    }
}
