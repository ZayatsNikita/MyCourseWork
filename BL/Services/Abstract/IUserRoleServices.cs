using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BL.DtoModels;

namespace BL.Services.Abstract
{
    public interface IUserRoleServices
    {
        public void Create(UserRole userRole);
        public List<UserRole> Read(
            int minId = -1,
            int maxId = -1,
            int minUserId = -1,
            int maxUserId = -1,
            int minRoleId = -1,
            int maxRoleId = -1
            );
        public void Delete(UserRole userRole);
        public void Update(UserRole userRole, int userId = -1, int roleId = -1);
    }
}