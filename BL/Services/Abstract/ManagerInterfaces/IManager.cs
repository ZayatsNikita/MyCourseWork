using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Services.Abstract
{
    public interface IManager<T>
    {
        public int Create(T dtoModel);

        public List<T> Read();

        public T ReadById(int id);

        public void Delete(int id);

        public void Update(T componet);
    }
}
