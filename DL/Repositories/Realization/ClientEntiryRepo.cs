using DL.Entities;

using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
namespace DL.Repositories
{
    public class ClientEntiryRepo : Abstract.IClientEntiryRepo
    {
        private MySqlConnection connection;
        private string addString = "INSERT INTO Client(Title, ContactInformation) values (@title, @c_info);SELECT LAST_INSERT_ID();";
        private string deleteString = "Delete from Client where id=@id; ";
        private string readString = "select * from client ";
        private string updateString = "update client ";
        public ClientEntiryRepo(string connectionString = @"Server=localhost;Port=3306;Database=work_fac;Uid=ForSomeCase;password=Kukrakuska713")  
        {
            connection = new MySqlConnection(connectionString);
        }
        public void Create(ClientEntity client)
        {
            connection.Open();
            MySqlCommand command = new MySqlCommand(addString);
            MySqlParameter titleParam = new MySqlParameter("@title", client.Title);
            MySqlParameter contactInfoParam = new MySqlParameter("@c_info", client.ContactInformation);
            
            command.Parameters.Add(titleParam);
            command.Parameters.Add(contactInfoParam);
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
                client.Id = id;
            }
            else
            {
                throw new ArgumentException("error of creating");
            }
        }

        public void Delete(ClientEntity clientEntity)
        {
            connection.Open();
            MySqlCommand command = new MySqlCommand(deleteString);
            MySqlParameter parameter = new MySqlParameter("@id", clientEntity.Id.ToString());
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

        public List<ClientEntity> Read(int MinId=-1, int MaxId=-1, string title=null, string contactInformation = null)
        {
            string stringWithWhere = null;
            try
            {
                stringWithWhere = CreateWherePartForReadQuery(MinId, MaxId, title, contactInformation);
            }
            catch (Exception)
            {
                throw;
            }
            MySqlCommand command= new MySqlCommand(readString+ stringWithWhere);
            command.Connection = connection;
            
            connection.Open();
            MySqlDataReader reader =  command.ExecuteReader();
            List<ClientEntity> result = new List<ClientEntity>();
            while (reader.Read())
            {
                object id = reader["id"];
                object titleFromDb = reader["Title"];
                object ContactInformation = reader["ContactInformation"];
                ClientEntity client = new ClientEntity
                {
                    Id = System.Convert.ToInt32(id),
                    Title = System.Convert.ToString(titleFromDb),
                    ContactInformation = System.Convert.ToString(ContactInformation)
                };
                result.Add(client);
            }
            connection.Close();
            
            return result;
        }

        public void Update(ClientEntity clientEntity, string title = null, string contactInformation = null)
        {
            connection.Open();
            
            string setString = CreateSetPartForUpdateQuery(title, contactInformation);
            
            MySqlCommand command = new MySqlCommand(updateString + setString + $" where id = {clientEntity.Id};");

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

        private string CreateWherePartForReadQuery(int MinId , int MaxId , string title , string contactInformation)
        {
            if (MinId > MaxId)
            {
                throw new ArgumentException("Wrong id params");
            }
            StringBuilder query;
            if(MinId!=-1 || MaxId!= -1 || title!=null || contactInformation!=null)
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
                if (contactInformation != null)
                {
                    if (query.Length > 6)
                    {
                        query.Append(" and ");
                    }
                    query.Append(" contactInformation = \"" + contactInformation+"\"");
                }
                return query.ToString();
            }
            else
            {
                return null;
            }
        }
        private string CreateSetPartForUpdateQuery(string title, string contactInfo)
        {
            if(title==null && contactInfo == null)
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
                if (contactInfo != null)
                {
                    if(title!=null)
                    {
                        where.Append(" , ");
                    }
                    where.Append("contactInformation = \"" + contactInfo+"\"");
                }
                return where.ToString();
            }
        }
    }

   


}
