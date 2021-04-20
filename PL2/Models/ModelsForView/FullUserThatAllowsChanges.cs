using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PL.Models.ModelsForView
{
    public class FullUserThatAllowsChanges
    {
        public string WorkerFio { get; set; }
        public int WorkerId { get; set; }
        public int UserId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool HasAnAccount { get; set; }
        public bool[] ActivatedRoles { get; set; }
        public List<Role> ExistingRoles { get; set; }
    }
}
