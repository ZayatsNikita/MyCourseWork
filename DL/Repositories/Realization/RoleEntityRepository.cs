using DL.Entities;
using DL.Extensions;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
namespace DL.Repositories
{
    public class RoleEntityRepository : Abstract.Repository, Abstract.IRoleEntityRepository
    {
        private string addString = "INSERT INTO Role(Title, Description, userid) values (@title, @descrip, @user_id);SELECT LAST_INSERT_ID();";
        private string deleteString = "Delete from Role where id=@id; ";
        private string readString = "select * from Role ";
        private string updateString = "update Role ";
        public RoleEntityRepository(string connectionString) : base(connectionString) {; }
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
            finally
            {
                connection.Close();
            }
            int id = Convert.ToInt32(obj);
            role.Id = id;
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
            finally
            {
                connection.Close();
            }
        }

        public List<RoleEntity> Read(int MinId= DefValInt, int MaxId= DefValInt, string title=null, string Description = null, int minAccsesLevel = DefValInt, int maxAccsesLevel = DefValInt)
        {
            string stringWithWhere = CreateWherePartForReadQuery(MinId, MaxId, title, Description, minAccsesLevel, maxAccsesLevel);
            
            MySqlCommand command= new MySqlCommand(readString + stringWithWhere);
            
            command.Connection = connection;
            

            List<RoleEntity> result = new List<RoleEntity>();

            connection.Open();

            try
            {
                MySqlDataReader reader = command.ExecuteReader();

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
            }
            finally
            {
                connection.Close();
            }
            return result;
        }

        public void Update(RoleEntity role, string title = null, string Description = null, int userId=DefValInt)
        {
            connection.Open();
            
            string setString = CreateSetPartForUpdateQuery(title, Description, userId);
            
            MySqlCommand command = new MySqlCommand(updateString + setString + $" where id = {role.Id};");

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

        private string CreateWherePartForReadQuery(int minId , int maxId , string title , string description, int minAccsesLevel, int maxAccsesLevel)
        {
            if(minId!= DefValInt || maxId!= DefValInt || title!=null || description!=null || minAccsesLevel!= DefValInt || maxAccsesLevel!= DefValInt)
            {

                StringBuilder query = new StringBuilder();

                query.AddWhereWord();

                query.AddWhereParam(minId, maxId, "Id");

                query.AddWhereParam(title, "title");

                query.AddWhereParam(description, "description");

                return query.ToString();
            }
            else
            {
                return null;
            }
        }
        private string CreateSetPartForUpdateQuery(string title, string description, int accsesLevel)
        {
            if(title==null && description == null && accsesLevel == DefValInt)
            {
                return null;
            }
            else
            {
                StringBuilder query = new StringBuilder();
                
                query.AddSetWord();
                
                query.AddSetParam(title, "title");
                
                query.AddSetParam(description, "Description");

                return query.ToString();
            }
        }
    }
}
