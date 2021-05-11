using System;
using System.Collections.Generic;
using System.Text;
using DL.Entities;
namespace DL.Repositories.Abstract
{
    public interface IUserRoleRepository
    {
        public void Create(UserRoleEntity userRole);
        public List<UserRoleEntity> Read(
            int minId,
            int maxId,
            int minUserId,
            int maxUserId,
            int minRoleId,
            int maxRoleId
            );
        public void Delete(UserRoleEntity userRole);
        public void Update(UserRoleEntity userRole, int userId = Repository.DefValInt, int roleId = Repository.DefValInt);
    }
}
