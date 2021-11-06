using System;
using System.Collections.Generic;
using System.Text;
namespace BL.DtoModels.Combined
{
    public class FullUser
    {
        public User User{get;set;}
        
        public Worker Worker { get; set; }

        public List<Role> Roles { get; set; }
    }
}
