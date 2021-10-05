﻿using DL.Entities;
using DL.Extensions;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
namespace DL.Repositories
{
    public class ComponetEntityRepository : Abstract.Repository, Abstract.IComponetEntityRepository
    {
        private string addString = "INSERT INTO Components(Title,productionStandards, Price) values (@title,@st, @price);SELECT LAST_INSERT_ID();";
        
        private string deleteString = "Delete from Components where id=@id; ";
        
        private string readString = "select * from Components ";
        
        private string updateString = "update Components ";

        public ComponetEntityRepository(string connectionString) : base(connectionString) {; }
        public void Create(ComponetEntity componet)
        {
            connection.Open();
            var command = new SqlCommand(addString);

            var titleParam = new SqlParameter("@title", componet.Title);
            var standartParam = new SqlParameter("@st", componet.ProductionStandards);
            var priceParam = new SqlParameter("@price", componet);

            command.Parameters.Add(titleParam);
            command.Parameters.Add(priceParam);
            command.Parameters.Add(standartParam);

            command.Connection = connection;

            object obj= null;
            try
            {
               obj = command.ExecuteScalar();
            }
            finally
            {
                connection.Close();
            }

            int id = Convert.ToInt32(obj);
            componet.Id = id;
        }

        public void Delete(ComponetEntity component)
        {
            connection.Open();
            var command = new SqlCommand(deleteString);
            var parameter = new SqlParameter("@id", component.Id.ToString());
            command.Parameters.Add(parameter);
            command.Connection = connection;
            try
            {
                int delCount = command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }

        public List<ComponetEntity> Read(int minId, int maxId,  string title, string productionStandards, decimal minPrice, decimal maxPrice)
        {
            string stringWithWhere = CreateWherePartForReadQuery(minId, maxId, title, productionStandards, minPrice, maxPrice);
            
            var command= new SqlCommand(readString + stringWithWhere);
            
            command.Connection = connection;

            List<ComponetEntity> result = new List<ComponetEntity>();

            try
            {
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    object id = reader["id"];
                    object titleFromDb = reader["Title"];
                    object priceFromDb = reader["Price"];
                    object productionStandardsFromDb = reader["productionStandards"];
                    ComponetEntity component = new ComponetEntity
                    {
                        Id = System.Convert.ToInt32(id),
                        Title = System.Convert.ToString(titleFromDb),
                        Price = System.Convert.ToDecimal(priceFromDb),
                        ProductionStandards = System.Convert.ToString(productionStandardsFromDb)
                    };
                    result.Add(component);
                }
            }
            finally
            {
                connection.Close();
            }
            return result;
        }

        public void Update(ComponetEntity componet, string title, string productionStandards, decimal price)
        {
            connection.Open();
            
            string setString = CreateSetPartForUpdateQuery(title, productionStandards, price);
            
            var command = new SqlCommand(updateString + setString + $" where id = {componet.Id};");

            command.Connection = connection;
            try
            {
                int updateCount = command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }

        private string CreateWherePartForReadQuery(int minId , int maxId , string title, string productionStandards, decimal minPrice, decimal maxPrice)
        {
            if(minId!= DefValInt || maxId!= DefValInt || title!=null || minPrice != DefValDec || maxPrice != DefValDec)
            {
                StringBuilder query;

                query = new StringBuilder();
                
                query.AddWhereWord();
                
                query.AddWhereParam(minId, maxId, "id");

                query.AddWhereParam(minPrice, maxPrice, "Price");
                
                query.AddWhereParam(title, "title");
                
                query.AddWhereParam(productionStandards, "productionStandards");
                
                return query.ToString();
            }
            else
            {
                return null;
            }
        }
        private string CreateSetPartForUpdateQuery(string title,string productionStandards, decimal price)
        {
            if(title==null && price == DefValDec)
            {
                return null;
            }
            else
            {
                StringBuilder where = new StringBuilder();
                
                where.AddSetWord();

                where.AddSetParam(title, "title");

                where.AddSetParam(productionStandards, "productionStandards");

                where.AddSetParam(price, "Price");
                
                return where.ToString();
            }
        }
    }
}
