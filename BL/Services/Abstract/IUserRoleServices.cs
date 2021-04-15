using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BL.dtoModels;

namespace BL.Services.Abstract
{
    public interface IUserRoleServices
    {
        public void Create(UserRole userRole);
        public List<UserRole> Read(
            int minId,
            int maxId,
            int minUserId,
            int maxUserId,
            int minRoleId,
            int maxRoleId
            );
        public void Delete(UserRole userRole);
        public void Update(UserRole userRole, int userId, int roleId);
    }
}