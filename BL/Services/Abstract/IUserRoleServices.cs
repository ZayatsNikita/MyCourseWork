using BL.DtoModels;
using System.Collections.Generic;

namespace BL.Services.Abstract
{
    public interface IUserRoleServices
    {
        public void Create(UserRole userRole);
        public List<UserRole> Read(
            int minId = Constants.DefIntVal,
            int maxId = Constants.DefIntVal,
            int minUserId = Constants.DefIntVal,
            int maxUserId = Constants.DefIntVal,
            int minRoleId = Constants.DefIntVal,
            int maxRoleId = Constants.DefIntVal
            );
        public void Delete(UserRole userRole);
        public void Update(UserRole userRole, int userId = Constants.DefIntVal, int roleId = Constants.DefIntVal);
    }
}