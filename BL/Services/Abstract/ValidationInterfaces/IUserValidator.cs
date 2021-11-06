using BL.DtoModels;

namespace BL.Services.Abstract.ValidationInterfaces
{
    public interface IUserValidator
    {
        void CheckForValidity(User user);
    }
}
