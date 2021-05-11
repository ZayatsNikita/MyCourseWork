using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BL.DtoModels;

namespace BL.Services.Abstract
{
    public interface IRoleServices
    {
        public void Create(Role role);
        public List<Role> Read(
            int minId= Constants.DefIntVal,
            int maxId = Constants.DefIntVal,
            string title = null,
            string description = null,
            int minAccsesLevel = Constants.DefIntVal,
            int maxAccsesLevel = Constants.DefIntVal
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