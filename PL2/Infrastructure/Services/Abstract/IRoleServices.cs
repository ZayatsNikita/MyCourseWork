using PL.Models;
using System.Collections.Generic;

namespace PL.Infrastructure.Services.Abstract
{
    public interface IRoleServices
    {
        public void Create(Role role);
        public List<Role> Read(
            int minId=-1,
            int maxId = -1,
            string title = null,
            string description = null,
            int minAccsesLevel = -1,
            int maxAccsesLevel = -1
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