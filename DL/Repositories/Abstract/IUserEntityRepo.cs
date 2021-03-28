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
            int minId,
            int maxId,
            string login,
            string password,
            int workerId
            );
        public void Delete(UserEntity user);
        public void Update(UserEntity clientEntity, string login, string password, int workerId);
    }
}