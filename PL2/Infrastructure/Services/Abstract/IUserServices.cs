using PL.Models;
using System.Collections.Generic;

namespace PL.Infrastructure.Services.Abstract
{
    public interface IUserServices
    {
        public void Create(User user);
        public List<User> Read(
            int minId = -1,
            int maxId = -1,
            string login = null,
            string password = null,
            int workerId = -1
            );
        public void Delete(User user);
        public void Update(User user, string login, string password, int workerId);
    }
}