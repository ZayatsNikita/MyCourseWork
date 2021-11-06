using BL.DtoModels;

namespace BL.Services.Abstract.ValidationInterfaces
{
    public interface IComponentValidator
    {
        void CheckForValidity(Component componet);
    }
}
