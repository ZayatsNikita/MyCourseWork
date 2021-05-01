﻿using System;
using System.Collections.Generic;
using System.Text;
using BL.DtoModels;
using BL.Exceptions;
namespace BL.Services.Validaton
{
    public class ServiceValidationService
    {
        public static bool IsValid(Service service)
        {
            if( service!= null)
            {
                if(service.Price> 100000 || service.Price < (decimal)0.01)
                {
                    throw new ValidationException(Messages.PriceMessage);
                }
                if ((service?.Title?.Length ?? 0) < 3 || service.Title.Length > 100)
                {
                    throw new ValidationException(Messages.WrongServiceTitlteMessage);
                }
                if ((service?.Description?.Length ?? 0) < 3 || service.Description.Length > 200)
                {
                    throw new ValidationException(Messages.WrongServiceDescritionMessage);
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
