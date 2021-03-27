using System;
using DL.Repositories.Abstract;
using DL.Repositories;
using DL.Entities;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ClientEntiryRepo repo = new ClientEntiryRepo();
            ClientEntity entity = new ClientEntity { Id = 1, ContactInformation = "life number: +375255464756", Title = "Valere" };
            repo.Create(entity);
        }
    }
}
