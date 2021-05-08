using DL.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using DL.Extensions;
namespace DL.Repositories
{
    public class ServiceEntityRepository : Abstract.IServiceEntityRepository
    {
        private MySqlConnection connection;
        private string addString = "INSERT INTO Service(Title, Description, Price) values (@title, @description, @price);SELECT LAST_INSERT_ID();";
        private string deleteString = "Delete from Service where id=@id; ";
        private string readString = "select * from Service ";
        private string updateString = "update Service ";
        public ServiceEntityRepository(string connectionString)  
        {
            connection = new MySqlConnection(connectionString);
        }
        public void Create(ServiceEntity service)
        {
            connection.Open();
            MySqlCommand command = new MySqlCommand(addString);
            MySqlParameter titleParam = new MySqlParameter("@title", service.Title);
            MySqlParameter descriptionInfoParam = new MySqlParameter("@description", service.Description);
            MySqlParameter priceParam = new MySqlParameter("@price", service.Price.ToString().Replace(',','.'));
           
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
            MySqlCommand command = new MySqlCommand(deleteString);
            MySqlParameter parameter = new MySqlParameter("@id", service.Id.ToString());
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

        public List<ServiceEntity> Read(int minId=-1, int maxId=-1, string title=null, string description = null, decimal maxPrice = -1, decimal minPrice = -1)
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
            MySqlCommand command= new MySqlCommand(readString+ stringWithWhere);
            command.Connection = connection;
            
            connection.Open();

            List<ServiceEntity> result = new List<ServiceEntity>();

            try
            {
                MySqlDataReader reader = command.ExecuteReader();
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

        public void Update(ServiceEntity service, string title=null, string description=null, decimal price=-1)
        {
            connection.Open();
            
            string setString = CreateSetPartForUpdateQuery(title, description, price);
            
            MySqlCommand command = new MySqlCommand(updateString + setString + $" where id = {service.Id} ");

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
            if(MinId!=-1 || MaxId!= -1 || title!=null || description!=null || maxPrice!=-1 || minPrice!=-1)
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
