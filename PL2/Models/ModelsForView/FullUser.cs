using System.Collections.Generic;

namespace PL.Models.ModelsForView
{
    public class FullUser
    {
        public User User{get;set;}
        public Worker Worker { get; set; }
        public List<Role> Roles { get; set; }
    }
}
