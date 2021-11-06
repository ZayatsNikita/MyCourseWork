using BL.DtoModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Services.Abstract.ValidationInterfaces
{
    public interface IClientValidator
    {
        void CheckForValidity(Client client);
    }
}
