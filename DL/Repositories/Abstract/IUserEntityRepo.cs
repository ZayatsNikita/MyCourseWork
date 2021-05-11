using DL.Entities;
using System.Collections.Generic;

namespace DL.Repositories.Abstract
{
    public interface IUserEntityRepo
    {
        public UserEntity Create(UserEntity user);
        public List<UserEntity> Read(
            int minId = Repository.DefValInt,
            int maxId = Repository.DefValInt,
            string login = null,
            string password = null,
            int workerId = Repository.DefValInt
            );
        public void Delete(UserEntity user, int workerId = -1);
        public void Update(UserEntity clientEntity, string login=null, string password=null, int workerId=-1);
    }
}