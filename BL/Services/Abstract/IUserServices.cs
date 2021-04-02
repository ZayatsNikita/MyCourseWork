using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BL.dtoModels;

namespace BL.Services.Abstract
{
    public interface IUserServices
    {
        public void Create(User user);
        public List<User> Read(
            int minId,
            int maxId,
            string login,
            string password,
            int workerId
            );
        public void Delete(User user);
        public void Update(User user, string login, string password, int workerId);
    }
}