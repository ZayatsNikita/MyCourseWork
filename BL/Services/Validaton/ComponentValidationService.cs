using System;
using System.Collections.Generic;
using System.Text;
using BL.DtoModels;
using BL.Exceptions;
namespace BL.Services.Validaton
{
    public class ComponentValidationService
    {
        public static bool IsValid(Componet componet)
        {
            if( componet!= null)
            {
                if(componet.Price> 100000 || componet.Price < (decimal)0.01)
                {
                    throw new ValidationException(Messages.PriceMessage);
                }
                if ((componet?.Title?.Length ?? 0) < 3 || componet.Title.Length > 100)
                {
                    throw new ValidationException(Messages.WrongServiceTitlteMessage);
                }
                if ((componet?.ProductionStandards?.Length ?? 0) < 3 || componet.ProductionStandards.Length > 100)
                {
                    throw new ValidationException(Messages.WrongStandartLentghtMessage);
                }
                return true;
            }
            else
            {
                throw new ValidationException(Messages.ObjectNotCreatedMessage);
            }
        }
    }
}
