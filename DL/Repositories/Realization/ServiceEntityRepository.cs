using DL.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
namespace DL.Repositories
{
    public class ServiceEntityRepository : Abstract.IServiceEntityRepository
    {
        private MySqlConnection connection;
        private string addString = "INSERT INTO Service(Title, Description, Price) values (@title, @description, @price);SELECT LAST_INSERT_ID();";
        private string deleteString = "Delete from Service where id=@id; ";
        private string readString = "select * from Service ";
        private string updateString = "update Service ";
        public ServiceEntityRepository(string connectionString = @"Server=localhost;Port=3306;Database=work_fac;Uid=ForSomeCase;password=Kukrakuska713")  
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
            catch(Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
            if(obj!=null)
            {

                int id = Convert.ToInt32(obj);
                service.Id = id;
            }
            else
            {
                throw new ArgumentException("error of creating");
            }
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
            catch(Exception) {
                throw;
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
            MySqlDataReader reader =  command.ExecuteReader();
            List<ServiceEntity> result = new List<ServiceEntity>();
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
            connection.Close();
            
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
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        private string CreateWherePartForReadQuery(int MinId , int MaxId,  string title, string description, decimal maxPrice, decimal minPrice)
        {
            if (MinId > MaxId)
            {
                throw new ArgumentException("Wrong id params");
            }
            StringBuilder query;
            if(MinId!=-1 || MaxId!= -1 || title!=null || description!=null || maxPrice!=-1 || minPrice!=-1)
            {
                query = new StringBuilder();
                query.Append("where ");

                if (MaxId != MinId)
                {
                    if (MinId != -1)
                    {
                        query.Append(" id>" + MinId.ToString());
                    }
                    if (MaxId != -1)
                    {
                        if(MinId != -1)
                        {
                            query.Append(" and ");
                        }
                        query.Append(" id<" + MaxId.ToString());
                    }
                }
                else
                {
                    if (MaxId != -1)
                    {
                        query.Append(" id = " + MaxId.ToString());
                    }
                }
                
                if (title != null)
                {
                    if (query.Length > 6)
                    {
                        query.Append(" and ");
                    }
                    query.Append(" title = \"" + title + "\"");
                }
                
                if (description != null)
                {
                    if (query.Length > 6)
                    {
                        query.Append(" and ");
                    }
                    query.Append(" description = \"" + description+"\"");
                }
                
                
                #region priceFilter
                if (minPrice != maxPrice)
                {
                    if (query.Length > 6)
                    {
                        query.Append(" and ");
                    }
                    if (minPrice != -1)
                    {
                        query.Append(" price>" + minPrice.ToString());
                    }
                    if (MaxId != -1)
                    {
                        if (MinId != -1)
                        {
                            query.Append(" and ");
                        }
                        query.Append(" price <" + maxPrice.ToString());
                    }
                }
                else
                {
                    if (minPrice != -1)
                    {
                        if (query.Length > 6)
                        {
                            query.Append(" and ");
                        }
                        query.Append(" price = " + MaxId.ToString());
                    }
                }
                #endregion

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
                StringBuilder where = new StringBuilder();
                where.Append(" set ");
                if (title != null)
                {
                    where.Append("title = \'" + title + "\'");
                }
                if (description != null)
                {
                    if (title != null)
                    {
                        where.Append(" , ");
                    }
                    where.Append("description = \'" + description + "\'");
                }
                if (price != -1)
                {
                    if (where.Length > 5)
                    {
                        where.Append(" , ");
                    }
                    where.Append("price = " + price.ToString().Replace(',','.'));
                }
                return where.ToString();
            }
            else
            {
                return null;
            }
        }
    }

   


}
