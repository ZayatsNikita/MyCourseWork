using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Entities
{
    public class UserRoleEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}
