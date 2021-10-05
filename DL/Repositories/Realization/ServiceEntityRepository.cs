﻿using DL.Entities;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using DL.Extensions;
namespace DL.Repositories
{
    public class ServiceEntityRepository : Abstract.Repository, Abstract.IServiceEntityRepository
    {
        private string addString = "INSERT INTO Services(Title, Description, Price) values (@title, @description, @price);SELECT LAST_INSERT_ID();";
        private string deleteString = "Delete from Services where id=@id; ";
        private string readString = "select * from Services ";
        private string updateString = "update Services ";
        public ServiceEntityRepository(string connectionString)  : base(connectionString) { }
       
        public void Create(ServiceEntity service)
        {
            connection.Open();
            var command = new SqlCommand(addString);
            var titleParam = new SqlParameter("@title", service.Title);
            var descriptionInfoParam = new SqlParameter("@description", service.Description);
            var priceParam = new SqlParameter("@price", service.Price.ToString().Replace(',','.'));
           
            command.Parameters.Add(titleParam);
            command.Parameters.Add(descriptionInfoParam);
            command.Parameters.Add(priceParam);
            
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
            service.Id = id;
        }

        public void Delete(ServiceEntity service)
        {
            connection.Open();
            var command = new SqlCommand(deleteString);
            var parameter = new SqlParameter("@id", service.Id.ToString());
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

        public List<ServiceEntity> Read(int minId=DefValInt, int maxId= DefValInt, string title=null, string description = null, decimal maxPrice = DefValDec, decimal minPrice = DefValDec)
        {
            string stringWithWhere = null;
            try
            {
                stringWithWhere = CreateWherePartForReadQuery(minId, maxId, title, description, maxPrice , minPrice);
            }
            catch (Exception)
            {
                throw;
            }
            var command= new SqlCommand(readString+ stringWithWhere);
            command.Connection = connection;
            
            connection.Open();

            List<ServiceEntity> result = new List<ServiceEntity>();

            try
            {
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    object id = reader["id"];
                    object titleFromDb = reader["Title"];
                    object descriptionFromDb = reader["Description"];
                    object priceFromDb = reader["Price"];
                    ServiceEntity service = new ServiceEntity
                    {
                        Id = System.Convert.ToInt32(id),
                        Title = System.Convert.ToString(titleFromDb),
                        Description = System.Convert.ToString(descriptionFromDb),
                        Price = System.Convert.ToDecimal(priceFromDb),
                    };
                    result.Add(service);
                }
            }
            finally
            {
                connection.Close();
            }
            return result;
        }

        public void Update(ServiceEntity service, string title=null, string description=null, decimal price=DefValDec)
        {
            connection.Open();
            
            string setString = CreateSetPartForUpdateQuery(title, description, price);
            
            var command = new SqlCommand(updateString + setString + $" where id = {service.Id} ");

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

        private string CreateWherePartForReadQuery(int MinId , int MaxId,  string title, string description, decimal maxPrice, decimal minPrice)
        {
            if(MinId!= DefValInt || MaxId!= DefValInt || title!=null || description!=null || maxPrice!= DefValInt || minPrice!= DefValInt)
            {
                StringBuilder query = new StringBuilder();
                query.AddWhereWord();

                query.AddWhereParam(MinId, MaxId, "id");

                query.AddWhereParam(minPrice, maxPrice, "price");

                query.AddWhereParam(title, "title");

                query.AddWhereParam(description, "description");

                return query.ToString();
            }
            else
            {
                return null;
            }
        }
        private string CreateSetPartForUpdateQuery(string title, string description, decimal price)
        {
            if(title!=null || description != null)
            {
                StringBuilder query = new StringBuilder();
                
                query.AddSetWord();
                
                query.AddSetParam(title, "title");
                
                query.AddSetParam(description, "description");
                
                query.AddSetParam(price, "price");
                
                return query.ToString();
            }
            else
            {
                return null;
            }
        }
    }
}
