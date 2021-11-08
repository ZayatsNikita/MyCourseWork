using System;
using System.Collections.Generic;
using System.Text;
using DL.Repositories.Abstract;
using System.Linq;
using DL.Entities;
using Moq;

namespace UnitTests
{
    public class FakeGenericRepository<T, G> 
        where T : class, IRepository<G>
        where G : IIdEntity
    {
        private List<G> Items { get; set; }

        private int BigestId { get; set; }

        private Mock<T> _mock;

        public FakeGenericRepository()
        {
            Items = new List<G>();

            var mock = new Mock<T>();

            mock.Setup(x => x.Create(It.IsAny<G>()))
                .Callback<G>(z =>
                {
                    z.Id = ++BigestId;
                    Items.Add(z);
                })
                .Returns<G>(z => z.Id);


            mock.Setup(x => x.Delete(It.IsAny<int>()))
                .Callback<G>(z =>
                {
                    Items.Where(x => x.Id != z.Id).ToList();
                });

            mock.Setup(x => x.ReadById(It.IsAny<int>()))
                .Returns<int>(z =>
                {
                    return Items.First(x => x.Id == z);
                });

            mock.Setup(x => x.Update(It.IsAny<G>()))
                .Callback<G>(z =>
                {
                    var length = Items.Count;

                    for (int i = 0; i < length; i++)
                    {
                        if (Items[i].Id == z.Id)
                        {
                            Items[i] = z;

                            break;
                        }
                    }
                });

            _mock = mock;
        }

        public int Create(G model) => _mock.Object.Create(model);

        public List<G> Read() => _mock.Object.Read();

        public G ReadById(int id) => _mock.Object.ReadById(id);

        public void Delete(int id) => _mock.Object.Delete(id);

        public void Update(G model) => _mock.Object.Update(model);
    }
}
