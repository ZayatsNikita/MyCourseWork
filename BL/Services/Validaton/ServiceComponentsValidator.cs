using BL.DtoModels.Combined;
using BL.Exceptions;
using BL.Services.Abstract.ValidationInterfaces;
using DL.Repositories.Abstract;
using System.Linq;

namespace BL.Services.Validaton
{
    public class ServiceComponentsValidator : IServiceComponentsValidator
    {
        private IСomponetServiceEntityRepo _сomponetServiceEntityRepo;

        public ServiceComponentsValidator(IСomponetServiceEntityRepo сomponetServiceEntityRepo)
        {
            _сomponetServiceEntityRepo = сomponetServiceEntityRepo;
        }

        public void CheckForValidy(FullServiceComponents fullServiceComponents)
        {
            _ = fullServiceComponents ?? throw new ValidationException("ServiceComponent is null.");

            var allServicesComponents = _сomponetServiceEntityRepo.Read();

            if (allServicesComponents.Where(x => fullServiceComponents.Id != x.Id)
                .Any(x => x.ServiceId == fullServiceComponents.Service.Id && x.ComponetId == fullServiceComponents.Componet.Id))
            {
                throw new ValidationException(Messages.ExsistingCombination);
            }
        }
    }
}
