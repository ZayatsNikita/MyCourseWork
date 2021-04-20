using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PL.Models.ModelsForView;
using PL.Models;

namespace PL.Infrastructure.Extensions
{
    public static class FullUserConvertToChangedVersions
    {
        public static FullUserThatAllowsChanges ConvertToChenged(this FullUser user, List<Role> exsistingRoles)
        {
            FullUserThatAllowsChanges result = new FullUserThatAllowsChanges();
            
            result.WorkerFio = user.Worker.PersonalData;
            result.WorkerId = user.Worker.PassportNumber;
            
            result.HasAnAccount = user.User != null;
            result.ExistingRoles = exsistingRoles;

            int length = exsistingRoles.Count();
            result.ActivatedRoles = new bool[length];

            if (result.HasAnAccount)
            {
                result.UserId = user.User.Id;
                result.Login = user.User.Login;
                result.Password = user.User.Password;
               
              
                for (int i = 0; i < length; i++)
                {
                    if (user.Roles.Contains(exsistingRoles[i]))
                    {
                        result.ActivatedRoles[i] = true;
                    }
                    else
                    {
                        result.ActivatedRoles[i] = false;
                    }
                }
            }
            else
            {
                for (int i = 0; i < length; i++)
                {
                    result.ActivatedRoles[i] = false;
                }
            }
            return result;
        }
        public static FullUser ConvertToOrdinarFullUser(this FullUserThatAllowsChanges sorse, List<Role> exsistingRoles)
        {
            FullUser result = new FullUser();

            Worker worker = new Worker() {PassportNumber = sorse.WorkerId, PersonalData = sorse.WorkerFio };

            if (sorse.HasAnAccount)
            {
                result.User = new User { Login = sorse.Login, Password = sorse.Password, WorkerId = sorse.WorkerId, Id = sorse.UserId };
                result.Roles = new List<Role>();
                sorse.ExistingRoles = exsistingRoles;
                int length = sorse.ExistingRoles.Count();
                for (int i = 0; i < length; i++)
                {
                    if (sorse.ActivatedRoles[i] == true)
                    {
                        result.Roles.Add(sorse.ExistingRoles[i]);
                    }
                }
            }
            else
            {
                result.User = null;
            }
            result.Worker = worker;
            return result;
        }
    }
}
