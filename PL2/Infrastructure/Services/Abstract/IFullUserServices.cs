using PL.Models.ModelsForView;
using System.Collections.Generic;

namespace PL.Infrastructure.Services.Abstract
{
    public interface IFullUserServices
    {
        public void Create(FullUser fullUser);
        public FullUser Read(
            string login,
            string Password
            );

        FullUser Read(int workerNumber);
        void Delete(FullUser fullUser);
        void Update(FullUser user);
    }
}