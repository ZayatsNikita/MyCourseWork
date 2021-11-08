using DL.Entities;
using DL.Repositories.Abstract;
using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;

namespace UnitTests
{
    public class FakeWorkerRepository : IWorkerEntityRepo
    {
        private List<WorkerEntity> Items { get; set; }

        public FakeWorkerRepository(IEnumerable<WorkerEntity> items)
        {
            Items = new List<WorkerEntity>(items);
        }

        public int Create(WorkerEntity model)
        {
            Items.Add(model);

            return model.PassportNumber;
        }

        public List<WorkerEntity> Read() => Items;

        public WorkerEntity ReadById(int id) => Items.First(x => x.PassportNumber == id);

        public void Delete(int id)
        {
            Items = Items.Where(x => x.PassportNumber != id).ToList();
        }

        public void Update(WorkerEntity model)
        {
            var length = Items.Count;

            for (int i = 0; i < length; i++)
            {
                if (Items[i].PassportNumber == model.PassportNumber)
                {
                    Items[i] = model;

                    break;
                }
            }
        }
    }
}