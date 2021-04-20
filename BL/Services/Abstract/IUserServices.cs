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
        public User Create(User user);
        public List<User> Read(
            int minId = -1,
            int maxId = -1,
            string login = null,
            string password = null,
            int workerId = -1
            );
        public void Delete(User user, int workerId = -1);
        public void Update(User user, string login=null, string password=null, int workerId=-1);
    }
}