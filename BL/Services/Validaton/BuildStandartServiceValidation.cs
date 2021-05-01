using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using BL.DtoModels.Combined;
using BL.Exceptions;

namespace BL.Services.Validaton
{
    public class BuildStandartServiceValidation
    {
        public static bool IsValid(BuildStandart standart, List<BuildStandart> standarts)
        {
            if (standart != null && standarts!=null)
            {
                if(standarts.Any(x=>x.Service.Id == standart.Service.Id && x.Componet.Id == standart.Componet.Id))
                {
                    throw new ValidationException(Messages.ExsistingCombination);
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
