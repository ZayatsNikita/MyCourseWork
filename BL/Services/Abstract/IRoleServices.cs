using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BL.dtoModels;

namespace BL.Services.Abstract
{
    public interface IRoleServices
    {
        public void Create(Role role);
        public List<Role> Read(
            int MinId,
            int MaxId,
            string Title,
            string Description,
            int UserId
            );
        public void Delete(Role role);
        public void Update(
            Role role,
            string Title,
            string Description,
            int UserId
            );
    }
}