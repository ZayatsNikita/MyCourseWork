using PL.Models;
using System.Collections.Generic;

namespace PL.Infrastructure.Services.Abstract
{
    public interface IRoleServices
    {
        public void Create(Role role);
        public List<Role> Read();
        public void Delete(Role role);
        public void Update(Role role);
    }
}