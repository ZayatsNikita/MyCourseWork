using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
namespace DL.Repositories
{
    public class RoleEntityRepository : Abstract.IRoleEntityRepository
    {
        private MySqlConnection connection;
        private string addString = "INSERT INTO Role(Title, Description, userid) values (@title, @descrip, @user_id);SELECT LAST_INSERT_ID();";
        private string deleteString = "Delete from Role where id=@id; ";
        private string readString = "select * from Role ";
        private string updateString = "update Role ";
        //public ClientEntiryRepo(string connectionString = @"Driver={MySQL ODBC 5.3 Unicode Driver}; Server = localhost; Database = work_fac; UID = root; PWD = Kukrakuska713")
        public RoleEntityRepository(string connectionString = @"Server=localhost;Port=3306;Database=work_fac;Uid=ForSomeCase;password=Kukrakuska713")  
        {
            connection = new MySqlConnection(connectionString);
            //connection.ConnectionString = connectionString;
        }
        public void Create(RoleEntity role)
        {
            connection.Open();

            MySqlCommand command = new MySqlCommand(addString);
            MySqlParameter titleParam = new MySqlParameter("@title", role.Title);
            MySqlParameter descriptionParam = new MySqlParameter("@descrip", role.Description);
            MySqlParameter userIdParam = new MySqlParameter("@user_id", role.AccsesLevel);
            
            command.Parameters.Add(titleParam);
            command.Parameters.Add(descriptionParam);
            command.Parameters.Add(userIdParam);

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
                role.Id = id;
            }
            else
            {
                throw new ArgumentException("error of creating");
            }
        }

        public void Delete(RoleEntity role)
        {
            connection.Open();
            MySqlCommand command = new MySqlCommand(deleteString);
            MySqlParameter parameter = new MySqlParameter("@id", role.Id.ToString());
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

        public List<RoleEntity> Read(int MinId=-1, int MaxId=-1, string title=null, string Description = null, int minAccsesLevel = -1, int maxAccsesLevel = -1)
        {
            string stringWithWhere = null;
            try
            {
                stringWithWhere = CreateWherePartForReadQuery(MinId, MaxId, title, Description, minAccsesLevel, maxAccsesLevel);
            }
            catch (Exception)
            {
                throw;
            }
            MySqlCommand command= new MySqlCommand(readString + stringWithWhere);
            command.Connection = connection;
            
            connection.Open();
            MySqlDataReader reader =  command.ExecuteReader();
            List<RoleEntity> result = new List<RoleEntity>();
            while (reader.Read())
            {
                object id = reader["id"];
                object titleFromDb = reader["Title"];
                object descriptionFromDb = reader["Description"];
                object accsesLevelFromDb = reader["AccsesLevel"];
                RoleEntity role = new RoleEntity
                {
                    Id = System.Convert.ToInt32(id),
                    Title = System.Convert.ToString(titleFromDb),
                    Description = System.Convert.ToString(descriptionFromDb),
                    AccsesLevel = System.Convert.ToInt32(accsesLevelFromDb)
                };
                result.Add(role);
            }
            connection.Close();
            
            return result;
        }

        public void Update(RoleEntity role, string title = null, string Description = null, int userId=-1)
        {
            connection.Open();
            
            string setString = CreateSetPartForUpdateQuery(title, Description, userId);
            
            MySqlCommand command = new MySqlCommand(updateString + setString + $" where id = {role.Id};");

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

        private string CreateWherePartForReadQuery(int minId , int maxId , string title , string Description, int minAccsesLevel, int maxAccsesLevel)
        {
            if (minId > maxId)
            {
                throw new ArgumentException("Wrong id params");
            }
            StringBuilder query;
            if(minId!=-1 || maxId!= -1 || title!=null || Description!=null || minAccsesLevel!=-1 || maxAccsesLevel!=-1)
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

                #region titleFilter
                if (title != null)
                {
                    if (query.Length > 7)
                    {
                        query.Append(" and ");
                    }
                    query.Append(" title = \"" + title + "\"");
                }
                #endregion

                #region descriptionFilter
                if (Description != null)
                {
                    if (query.Length > 7)
                    {
                        query.Append(" and ");
                    }
                    query.Append(" description = \"" + Description+"\"");
                }
                #endregion

                #region AccsesLevelFilter
                if (minAccsesLevel != maxAccsesLevel)
                {
                    if (minAccsesLevel != -1)
                    {
                        if (query.Length > 7)
                        {
                            query.Append(" and ");
                        }
                        query.Append(" AccsesLevel >" + minAccsesLevel.ToString());
                    }
                    if (maxId != -1)
                    {
                        if (query.Length > 7)
                        {
                            query.Append(" and ");
                        }
                        query.Append("AccsesLevel <" + maxAccsesLevel.ToString());
                    }
                }
                else
                {
                    if (maxAccsesLevel != -1)
                    {
                        if (query.Length > 7)
                        {
                            query.Append(" and ");
                        }
                        query.Append(" AccsesLevel = " + maxAccsesLevel.ToString());
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
        private string CreateSetPartForUpdateQuery(string title, string Description, int accsesLevel)
        {
            if(title==null && Description == null && accsesLevel == -1)
            {
                return null;
            }
            else
            {
                StringBuilder where = new StringBuilder();
                where.Append(" set ");
                if (title != null)
                {
                    where.Append(" title = \"" + title+"\"");
                }
                if (Description != null)
                {
                    if(where.Length > 5)
                    {
                        where.Append(" , ");
                    }
                    where.Append(" Description = \"" + Description+"\"");
                }
                if (accsesLevel != -1)
                {
                    if (where.Length > 5)
                    {
                        where.Append(" , ");
                    }
                    where.Append(" AccsesLevel = " + accsesLevel);
                }
                return where.ToString();
            }
        }
    }
}
