using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DL.Entities;

namespace DL.Repositories.Abstract
{
    public interface IUserEntityRepo
    {
        public void Create(UserEntity user);
        public List<UserEntity> Read(
            int minId = -1,
            int maxId = -1,
            string login = null,
            string password = null,
            int workerId = -1
            );
        public void Delete(UserEntity user);
        public void Update(UserEntity clientEntity, string login, string password, int workerId);
    }
}