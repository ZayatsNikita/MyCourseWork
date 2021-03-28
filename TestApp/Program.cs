using System;
using DL.Repositories.Abstract;
using DL.Repositories;
using DL.Entities;
using System.Collections.Generic;
namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ClientEntiryRepo repo = new ClientEntiryRepo();
            //ClientEntity entity = new ClientEntity { Id = 1, ContactInformation = "life number: +375255464756", Title = "Nikita" };

            ClientEntity client = new ClientEntity { Id = 2 };
            

            List<ClientEntity> list = repo.Read(MinId: 1, MaxId:5, title: "GSTU",contactInformation: "70-957");

        }
    }
}
