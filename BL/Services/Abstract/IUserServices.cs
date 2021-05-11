using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BL.DtoModels;

namespace BL.Services.Abstract
{
    public interface IUserServices
    {
        public User Create(User user);
        public List<User> Read(
            int minId = Constants.DefIntVal,
            int maxId = Constants.DefIntVal,
            string login = null,
            string password = null,
            int workerId = Constants.DefIntVal
            );
        public void Delete(User user, int workerId = Constants.DefIntVal);
        public void Update(User user, string login=null, string password=null, int workerId= Constants.DefIntVal);
    }
}