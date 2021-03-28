using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DL.Entities;

namespace DL.Repositories.Abstract
{
    public interface IRoleEntity
    {
        public void Create(RoleEntity role);
        public List<RoleEntity> Read(
            int MinId,
            int MaxId,
            string Title,
            string Description,
            int UserId
            );
        public void Delete(RoleEntity role);
        public void Update(
            RoleEntity role,
            string Title,
            string Description,
            int UserId
            );
    }
}