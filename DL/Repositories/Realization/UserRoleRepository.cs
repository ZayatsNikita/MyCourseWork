using DL.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using DL.Extensions;

namespace DL.Repositories
{
    public class UserRoleRepository : Abstract.IUserRoleRepository
    {
        private MySqlConnection connection;
        private string addString = "INSERT INTO UserRole (UserId, RoleId) values (@UserId, @RoleId);SELECT LAST_INSERT_ID();";
        private string deleteString = "Delete from UserRole where id=@id";
        private string readString = "select * from UserRole ";
        private string updateString = "update UserRole ";
        public UserRoleRepository(string connectionString)  
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
            finally
            {
                connection.Close();
            }
            int id = Convert.ToInt32(obj);
            userRole.Id = id;
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
            finally
            {
                connection.Close();
            }
        }
        public List<UserRoleEntity> Read(int minId=-1, int maxId = -1, int minUserId = -1, int maxUserId = -1, int minRoleId = -1, int maxRoleId = -1)
        {
            string stringWithWhere = CreateWherePartForReadQuery(minId, maxId, minUserId, maxUserId, minRoleId, maxRoleId);
            MySqlCommand command = new MySqlCommand(readString + stringWithWhere);
            command.Connection = connection;

            connection.Open();
            List<UserRoleEntity> result = new List<UserRoleEntity>();
            try
            {
                MySqlDataReader reader = command.ExecuteReader();
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
            }
            finally
            {
                connection.Close();
            }
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
            finally
            {
                connection.Close();
            }
        }
        private string CreateWherePartForReadQuery(int minId, int maxId, int minUserId, int maxUserId, int minRoleId, int maxRoleId)
        {
            if(minId!=-1 || maxId!= -1 || minUserId != -1 || maxUserId != -1 || minRoleId != -1 || maxRoleId != -1)
            {
                StringBuilder query = new StringBuilder();
                query.AddWhereWord();

                query.AddWhereParam(minId, maxId, "id");

                query.AddWhereParam(minUserId, maxUserId, "UserId");

                query.AddWhereParam(minRoleId, maxRoleId, "RoleId");

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
                StringBuilder query = new StringBuilder();
                
                query.AddSetWord();
                
                query.AddSetParam(userId, "userId");

                query.AddSetParam(roleId, "roleId");
                
                return query.ToString();
            }
        }
    }
}
