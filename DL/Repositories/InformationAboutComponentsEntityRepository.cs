using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
namespace DL.Repositories
{
    public class InformationAboutComponentsEntityRepository : Abstract.IInformationAboutComponentsEntityRepository
    {
        private MySqlConnection connection;
        private string addString = "INSERT INTO InformationAboutComponents (ComponetId, CountOfComponents) values (@ComponetId, @CountOfComponents);SELECT LAST_INSERT_ID();";
        private string deleteString = "Delete from InformationAboutComponents where id=@id; ";
        private string readString = "select * from InformationAboutComponents ";
        private string updateString = "update InformationAboutComponents ";
        public InformationAboutComponentsEntityRepository(string connectionString = @"Server=localhost;Port=3306;Database=work_fac;Uid=ForSomeCase;password=Kukrakuska713")  
        {
            connection = new MySqlConnection(connectionString);
        }
        public void Create(InformationAboutComponentsEntity info)
        {
            connection.Open();
            MySqlCommand command = new MySqlCommand(addString);
            MySqlParameter componentParam = new MySqlParameter("@ComponetId", info.ComponetId);
            MySqlParameter countParam = new MySqlParameter("@CountOfComponents", info.CountOfComponents);
            
            command.Parameters.Add(componentParam);
            command.Parameters.Add(countParam);

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
                info.Id = id;
            }
            else
            {
                throw new ArgumentException("error of creating");
            }
        }

        public void Delete(InformationAboutComponentsEntity info)
        {
            connection.Open();
            MySqlCommand command = new MySqlCommand(deleteString);
            MySqlParameter parameter = new MySqlParameter("@id", info.Id.ToString());
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

        public List<InformationAboutComponentsEntity> Read(
            int minId=-1, int maxId=-1,

            int minComponetId=-1, int maxComponetId = -1,

            int minCountOfComponents = -1, int maxCountOfComponents = -1)
        {
            string stringWithWhere = null;
            try
            {
                stringWithWhere = CreateWherePartForReadQuery(minId, maxId, minComponetId, maxComponetId, minCountOfComponents, maxCountOfComponents);
            }
            catch (Exception)
            {
                throw;
            }
            MySqlCommand command= new MySqlCommand(readString + stringWithWhere);
            command.Connection = connection;
            
            connection.Open();
            MySqlDataReader reader =  command.ExecuteReader();
            List<InformationAboutComponentsEntity> result = new List<InformationAboutComponentsEntity>();
            while (reader.Read())
            {
                object id = reader["id"];
                object ComponetId = reader["ComponetId"];
                object CountOfComponents = reader["CountOfComponents"];
                InformationAboutComponentsEntity info = new InformationAboutComponentsEntity
                {
                    Id = System.Convert.ToInt32(id),
                    ComponetId = System.Convert.ToInt32(ComponetId),
                    CountOfComponents = System.Convert.ToInt32(CountOfComponents)
                };
                result.Add(info);
            }
            connection.Close();
            
            return result;
        }

        public void Update(InformationAboutComponentsEntity infoEntity, int componentId = -1, int CountOfComponents = -1)
        {
            connection.Open();
            
            string setString = CreateSetPartForUpdateQuery(componentId, CountOfComponents);
            
            MySqlCommand command = new MySqlCommand(updateString + setString + $" where id = {infoEntity.Id};");

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

        private string CreateWherePartForReadQuery(int minId,
            int maxId,

            int minComponetId,
            int maxComponetId,

            int minCountOfComponents,
            int maxCountOfComponents)
        {
            
            StringBuilder query;
            if(minId != -1 || maxId != -1 || minComponetId != -1 || maxComponetId != -1 || minCountOfComponents!=-1 || maxCountOfComponents!=-1)
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

                #region componentIdFilter
                if (minComponetId != maxComponetId)
                {
                    if (minComponetId != -1)
                    {
                        if (query.Length > 7)
                        {
                            query.Append(" and ");
                        }
                        query.Append(" ComponetId >" + minComponetId.ToString());
                    }
                    if (maxComponetId != -1)
                    {
                        if (query.Length > 7)
                        {
                            query.Append(" and ");
                        }
                        query.Append(" ComponetId <" + maxComponetId.ToString());
                    }
                }
                else
                {
                    if (maxComponetId != -1)
                    {
                        if (query.Length > 7)
                        {
                            query.Append(" and ");
                        }
                        query.Append(" ComponetId = " + maxComponetId.ToString());
                    }
                }
                #endregion

                #region count filter
                if (minCountOfComponents != maxCountOfComponents)
                {
                    if (minCountOfComponents != -1)
                    {
                        if (query.Length > 7)
                        {
                            query.Append(" and ");
                        }
                        query.Append(" CountOfComponents >" + minCountOfComponents.ToString());
                    }
                    if (maxId != -1)
                    {
                        if (query.Length > 7)
                        {
                            query.Append(" and ");
                        }
                        query.Append("CountOfComponents <" + maxCountOfComponents.ToString());
                    }
                }
                else
                {
                    if (maxCountOfComponents != -1)
                    {
                        if (query.Length > 7)
                        {
                            query.Append(" and ");
                        }
                        query.Append(" CountOfComponents = " + maxCountOfComponents.ToString());
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
        private string CreateSetPartForUpdateQuery(int CountOfComponents, int componentId)
        {
            if(CountOfComponents == -1 && componentId == -1)
            {
                return null;
            }
            else
            {
                StringBuilder where = new StringBuilder();
                where.Append(" set ");
                if (CountOfComponents != -1)
                {
                    where.Append("CountOfComponents = " + CountOfComponents);
                }
                if (componentId != -1)
                {
                    if(CountOfComponents != -1)
                    {
                        where.Append(" , ");
                    }
                    where.Append("componentId = " + componentId);
                }
                return where.ToString();
            }
        }
    }

   


}
