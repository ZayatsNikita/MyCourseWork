using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
namespace DL.Repositories
{
    public class ComponetEntityRepository : Abstract.IComponetEntityRepository
    {
        private MySqlConnection connection;
        private string addString = "INSERT INTO Componet(Title, Price) values (@title, @price);SELECT LAST_INSERT_ID();";
        private string deleteString = "Delete from Componet where id=@id; ";
        private string readString = "select * from Componet ";
        private string updateString = "update Componet ";
        public ComponetEntityRepository(string connectionString = @"Server=localhost;Port=3306;Database=work_fac;Uid=ForSomeCase;password=Kukrakuska713")  
        {
            connection = new MySqlConnection(connectionString);
        }
        public void Create(ComponetEntity componet)
        {
            connection.Open();
            MySqlCommand command = new MySqlCommand(addString);

            MySqlParameter titleParam = new MySqlParameter("@title", componet.Title);
            MySqlParameter priceParam = new MySqlParameter("@price", componet.Price.ToString());

            command.Parameters.Add(titleParam);
            command.Parameters.Add(priceParam);

            command.Parameters.Add(titleParam);
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
                componet.Id = id;
            }
            else
            {
                throw new ArgumentException("error of creating");
            }
        }

        public void Delete(ComponetEntity component)
        {
            connection.Open();
            MySqlCommand command = new MySqlCommand(deleteString);
            MySqlParameter parameter = new MySqlParameter("@id", component.Id.ToString());
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

        public List<ComponetEntity> Read(int minId = -1, int maxId = -1,  string title = null, decimal minPrice = -1, decimal maxPrice = -1)
        {
            string stringWithWhere = null;
            try
            {
                stringWithWhere = CreateWherePartForReadQuery(minId, maxId, title, minPrice, maxPrice);
            }
            catch (Exception)
            {
                throw;
            }
            MySqlCommand command= new MySqlCommand(readString+ stringWithWhere);
            command.Connection = connection;
            
            connection.Open();
            MySqlDataReader reader =  command.ExecuteReader();
            List<ComponetEntity> result = new List<ComponetEntity>();
            while (reader.Read())
            {
                object id = reader["id"];
                object titleFromDb = reader["Title"];
                object priceFromDb = reader["Price"];
                ComponetEntity component = new ComponetEntity
                {
                    Id = System.Convert.ToInt32(id),
                    Title = System.Convert.ToString(titleFromDb),
                    Price = System.Convert.ToDecimal(priceFromDb)
                };
                result.Add(component);
            }
            connection.Close();
            
            return result;
        }

        public void Update(ComponetEntity componet, string title=null, decimal price = -1)
        {
            connection.Open();
            
            string setString = CreateSetPartForUpdateQuery(title, price);
            
            MySqlCommand command = new MySqlCommand(updateString + setString + $" where id = {componet.Id};");

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

        private string CreateWherePartForReadQuery(int minId , int maxId , string title , decimal minPrice, decimal maxPrice)
        {
            if (minId > maxId)
            {
                throw new ArgumentException("Wrong id params");
            }
            StringBuilder query;
            if(minId!=-1 || maxId!= -1 || title!=null || minPrice != -1 || maxPrice != -1)
            {
                query = new StringBuilder();
                query.Append(" where ");

                #region IdFilter
                if (minId != maxId)
                {
                    if (minId != -1)
                    {
                        if (query.Length > 7)
                        {
                            query.Append(" and ");
                        }
                        query.Append(" Id >" + minId.ToString());
                    }
                    if (maxId != -1)
                    {
                        if (query.Length > 7)
                        {
                            query.Append(" and ");
                        }
                        query.Append("Id <" + maxId.ToString());
                    }
                }
                else
                {
                    if (maxId != -1)
                    {
                        if (query.Length > 7)
                        {
                            query.Append(" and ");
                        }
                        query.Append(" Id = " + maxId.ToString());
                    }
                }
                #endregion

                #region PriceFilter
                if (minPrice != maxPrice)
                {
                    if (minPrice != -1)
                    {
                        if (query.Length > 7)
                        {
                            query.Append(" and ");
                        }
                        query.Append(" Price >" + minPrice.ToString());
                    }
                    if (maxPrice != -1)
                    {
                        if (query.Length > 7)
                        {
                            query.Append(" and ");
                        }
                        query.Append(" Price <" + maxPrice.ToString());
                    }
                }
                else
                {
                    if (maxPrice != -1)
                    {
                        if (query.Length > 7)
                        {
                            query.Append(" and ");
                        }
                        query.Append(" Price = " + minPrice.ToString());
                    }
                }
                #endregion

                #region titleFilter
                if (title != null)
                {
                        if (query.Length > 7)
                        {
                            query.Append(" and ");
                        }
                        query.Append(" title = \"" + title +"\"");
                }
                #endregion

                return query.ToString();
            }
            else
            {
                return null;
            }
        }
        private string CreateSetPartForUpdateQuery(string title, decimal price)
        {
            if(title==null && price ==-1)
            {
                return null;
            }
            else
            {
                StringBuilder where = new StringBuilder();
                where.Append(" set ");
                if (title != null)
                {
                    where.Append("title = \"" + title+"\"");
                }
                if (price != -1)
                {
                    if(title!=null)
                    {
                        where.Append(" , ");
                    }
                    where.Append("price = " + price);
                }
                return where.ToString();
            }
        }
    }

   


}
