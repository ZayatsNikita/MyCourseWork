using BL.DtoModels.Combined;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Services.Abstract.ValidationInterfaces
{
    public interface IServiceComponentsValidator
    {
        void CheckForValidy(FullServiceComponents fullServiceComponents);
    }
}
