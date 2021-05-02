using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Exceptions
{
    public static class Messages
    {
        public const string ObjectNotCreatedMessage = "The object was not created";
        public const string PriceMessage = "The price value must belong to the range from 0,01 to 1 00 000";
        public const string WrongServiceTitlteMessage = "The length of the service title must be between 3 and 100 characters";
        public const string WrongServiceDescritionMessage = "The length of the service description must be between 3 and 200 characters";
        public const string ExsistingCombination = "A similar combination of component and service already exists";
        public const string ExsistingWorkerId = "A similar worker code is already used";
        public const string ExsistingLogin = "A similar login is already used";
        public const string WrongFIOLength = "The length of the worker FIO must be between 3 and 100 characters";
        public const string WrongLoginLength = "The length of the login must be between 3 and 25 characters";
        public const string WrongPasswordLength = "The length of the worker password must be between 3 and 25 characters";
        public const string NoRoleSelected = "The user must have at least one role. Change the state of the \"Has a role\" radio button to no, or specify roles.";



    }
}
