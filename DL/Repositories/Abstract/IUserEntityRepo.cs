using DL.Entities;
using System.Collections.Generic;

namespace DL.Repositories.Abstract
{
    public interface IUserEntityRepo
    {
        public UserEntity Create(UserEntity user);
        public List<UserEntity> Read(
            int minId = -1,
            int maxId = -1,
            string login = null,
            string password = null,
            int workerId = -1
            );
        public void Delete(UserEntity user, int workerId = -1);
        public void Update(UserEntity clientEntity, string login=null, string password=null, int workerId=-1);
    }
}