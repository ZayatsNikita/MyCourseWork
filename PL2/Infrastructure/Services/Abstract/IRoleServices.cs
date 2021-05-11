using PL.Models;
using System.Collections.Generic;

namespace PL.Infrastructure.Services.Abstract
{
    public interface IRoleServices
    {
        public void Create(Role role);
        public List<Role> Read(
            int minId= Constans.DefIntVal,
            int maxId = Constans.DefIntVal,
            string title = null,
            string description = null,
            int minAccsesLevel = Constans.DefIntVal,
            int maxAccsesLevel = Constans.DefIntVal
            );
        public void Delete(Role role);
        public void Update(
            Role role,
            string Title,
            string Description,
            int accsesLevel
            );
    }
}