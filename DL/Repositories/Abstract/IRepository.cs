using System.Collections.Generic;

namespace DL.Repositories.Abstract
{
    public interface IRepository<T>
    {
        public int Create(T model);

        public List<T> Read();

        public T ReadById(int id);

        public void Delete(int id);

        public void Update(T model);
    }
}
