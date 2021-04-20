using DL.Entities;

using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
namespace DL.Repositories
{
    public class UserRoleRepository : Abstract.IUserRoleRepository
    {
        private MySqlConnection connection;
        private string addString = "INSERT INTO UserRole (UserId, RoleId) values (@UserId, @RoleId);SELECT LAST_INSERT_ID();";
        private string deleteString = "Delete from UserRole where id=@id";
        private string readString = "select * from UserRole ";
        private string updateString = "update UserRole ";
        public UserRoleRepository(string connectionString = @"Server=localhost;Port=3306;Database=work_fac;Uid=ForSomeCase;password=Kukrakuska713")  
        {
            connection = new MySqlConnection(connectionString);
        }
        public void Create(UserRoleEntity userRole)
        {
            connection.Open();
            MySqlCommand command = new MySqlCommand(addString);
            MySqlParameter titleParam = new MySqlParameter("@UserId", userRole.UserId);
            MySqlParameter contactInfoParam = new MySqlParameter("@RoleId", userRole.RoleId);
            
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
                userRole.Id = id;
            }
            else
            {
                throw new ArgumentException("error of creating");
            }
        }

        public void Delete(UserRoleEntity userRole)
        {
            connection.Open();
            MySqlCommand command = new MySqlCommand(deleteString);
            MySqlParameter parameter = new MySqlParameter("@id", userRole.Id.ToString());
            command.Parameters.Add(parameter);
            command.Connection = connection;
            try
            {
                int delCount = command.ExecuteNonQuery();
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
        public List<UserRoleEntity> Read(int minId, int maxId, int minUserId, int maxUserId, int minRoleId, int maxRoleId)
        {
            string stringWithWhere = null;
            try
            {
                stringWithWhere = CreateWherePartForReadQuery(minId, maxId, minUserId, maxUserId, minRoleId, maxRoleId);
            }
            catch (Exception)
            {
                throw;
            }
            MySqlCommand command = new MySqlCommand(readString + stringWithWhere);
            command.Connection = connection;

            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            List<UserRoleEntity> result = new List<UserRoleEntity>();
            while (reader.Read())
            {
                object idFromDb = reader["Id"];
                object userIdFromDb = reader["UserId"];
                object roleIdFromDb = reader["RoleId"];
                UserRoleEntity userRole = new UserRoleEntity
                {
                    Id = System.Convert.ToInt32(idFromDb),
                    UserId = System.Convert.ToInt32(userIdFromDb),
                    RoleId = System.Convert.ToInt32(roleIdFromDb)
                };
                result.Add(userRole);
            }
            connection.Close();

            return result;
        }


        public void Update(UserRoleEntity userRole, int userId = -1, int roleId = -1)
        {
            connection.Open();
            
            string setString = CreateSetPartForUpdateQuery(userId, roleId);
            
            MySqlCommand command = new MySqlCommand(updateString + setString + $" where id = {userRole.Id};");

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


        private string CreateWherePartForReadQuery(int minId, int maxId, int minUserId, int maxUserId, int minRoleId, int maxRoleId)
        {
            if (minId > maxId)
            {
                throw new ArgumentException("Wrong id params");
            }
            StringBuilder query;
            if(minId!=-1 || maxId!= -1 || minUserId != -1 || maxUserId != -1 || minRoleId != -1 || maxRoleId != -1)
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

                #region UserIdFilter
                if (minUserId != maxUserId)
                {
                    if (minUserId != -1)
                    {
                        if (query.Length > 7)
                        {
                            query.Append(" and ");
                        }
                        query.Append(" UserId >" + minUserId.ToString());
                    }
                    if (maxUserId != -1)
                    {
                        if (query.Length > 7)
                        {
                            query.Append(" and ");
                        }
                        query.Append(" UserId <" + maxUserId.ToString());
                    }
                }
                else
                {
                    if (maxUserId != -1)
                    {
                        if (query.Length > 7)
                        {
                            query.Append(" and ");
                        }
                        query.Append(" UserId = " + maxUserId.ToString());
                    }
                }
                #endregion

                #region RoleIdFilter
                if (minRoleId != maxRoleId)
                {
                    if (minRoleId != -1)
                    {
                        if (query.Length > 7)
                        {
                            query.Append(" and ");
                        }
                        query.Append(" RoleId >" + minRoleId.ToString());
                    }
                    if (maxRoleId != -1)
                    {
                        if (query.Length > 7)
                        {
                            query.Append(" and ");
                        }
                        query.Append(" RoleId <" + maxRoleId.ToString());
                    }
                }
                else
                {
                    if (maxRoleId != -1)
                    {
                        if (query.Length > 7)
                        {
                            query.Append(" and ");
                        }
                        query.Append(" RoleId = " + maxRoleId.ToString());
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
        private string CreateSetPartForUpdateQuery(int userId, int roleId)
        {
            if(userId==-1 && roleId == -1)
            {
                return null;
            }
            else
            {
                StringBuilder where = new StringBuilder();
                where.Append(" set ");
                if (userId != -1)
                {
                    where.Append("userId = " + userId.ToString());
                }
                if (roleId != -1)
                {
                    if(where.Length > 5)
                    {
                        where.Append(" , ");
                    }
                    where.Append("roleId = " + roleId.ToString());
                }
                return where.ToString();
            }
        }
    }
}
