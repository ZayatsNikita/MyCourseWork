using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
namespace DL.Repositories
{
    public class UserEntityRepo : Abstract.IUserEntityRepo
    {
        private MySqlConnection connection;
        private string addString = "INSERT INTO User (Login, Password, WorkerId) values (@login, @password, @worker_id);SELECT LAST_INSERT_ID();";
        private string deleteString = "Delete from User where id=@id; ";
        private string readString = "select * from User ";
        private string updateString = "update User ";
        //public ClientEntiryRepo(string connectionString = @"Driver={MySQL ODBC 5.3 Unicode Driver}; Server = localhost; Database = work_fac; UID = root; PWD = Kukrakuska713")
        public UserEntityRepo(string connectionString = @"Server=localhost;Port=3306;Database=work_fac;Uid=ForSomeCase;password=Kukrakuska713")
        {
            connection = new MySqlConnection(connectionString);
        }
        public void Create(UserEntity user)
        {
            connection.Open();
            MySqlCommand command = new MySqlCommand(addString);
            MySqlParameter loginParam = new MySqlParameter("@login", user.Login);
            MySqlParameter passwordParam = new MySqlParameter("@password", user.Password);
            MySqlParameter workerParam = new MySqlParameter("@worker_id", user.WorkerId);

            command.Parameters.Add(loginParam);
            command.Parameters.Add(passwordParam);
            command.Parameters.Add(workerParam);

            command.Connection = connection;

            object obj = null;
            try
            {
                obj = command.ExecuteScalar();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
            if (obj != null)
            {

                int id = Convert.ToInt32(obj);
                user.Id = id;
            }
            else
            {
                throw new ArgumentException("error of creating");
            }
        }

        public void Delete(UserEntity user)
        {
            connection.Open();
            MySqlCommand command = new MySqlCommand(deleteString);
            MySqlParameter parameter = new MySqlParameter("@id", user.Id.ToString());
            command.Parameters.Add(parameter);
            command.Connection = connection;
            try
            {
                int delCount = command.ExecuteNonQuery();
            }
            catch (Exception) {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public List<UserEntity> Read(int MinId = -1, int MaxId = -1, string login = null, string password = null, int workerId = -1)
        {
            string stringWithWhere = null;
            try
            {
                stringWithWhere = CreateWherePartForReadQuery(MinId, MaxId, login, password, workerId);
            }
            catch (Exception)
            {
                throw;
            }
            MySqlCommand command = new MySqlCommand(readString + stringWithWhere);
            command.Connection = connection;

            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            List<UserEntity> result = new List<UserEntity>();
            while (reader.Read())
            {
                object id = reader["id"];
                object passwordFromDb = reader["password"];
                object loginInformation = reader["login"];
                object workerIDInformation = reader["workerId"];

                UserEntity user = new UserEntity
                {
                    Id = System.Convert.ToInt32(id),
                    Password = System.Convert.ToString(passwordFromDb),
                    Login = System.Convert.ToString(loginInformation),
                    WorkerId = System.Convert.ToInt32(workerIDInformation)
                };
                result.Add(user);
            }
            connection.Close();

            return result;
        }

        public void Update(UserEntity user, string login = null, string password = null, int workerId = -1)
        {
            connection.Open();

            string setString = CreateSetPartForUpdateQuery(login, password, workerId);

            MySqlCommand command = new MySqlCommand(updateString + setString + $" where id = {user.Id};");

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

        private string CreateWherePartForReadQuery(int MinId, int MaxId, string login, string password, int workerId)
        {
            if (MinId > MaxId)
            {
                throw new ArgumentException("Wrong id params");
            }
            StringBuilder query;
            if (MinId != -1 || MaxId != -1 || login != null || password != null || workerId!=-1)
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
                if (login != null)
                {
                    if (query.Length > 6)
                    {
                        query.Append(" and ");
                    }
                    query.Append(" login = \"" + login + "\"");
                }
                if (password != null)
                {
                    if (query.Length > 6)
                    {
                        query.Append(" and ");
                    }
                    query.Append(" password = \"" + password+"\"");
                }
                if (workerId != -1)
                {
                    if (query.Length > 6)
                    {
                        query.Append(" and ");
                    }
                    query.Append(" UserId = " + workerId.ToString());
                }
                return query.ToString();
            }
            else
            {
                return null;
            }
        }
        private string CreateSetPartForUpdateQuery(string login, string password, int workerId)
        {
            if(login==null && password == null)
            {
                return null;
            }
            else
            {
                StringBuilder where = new StringBuilder();
                where.Append(" set ");
                if (login != null)
                {
                    where.Append("login = \"" + login+"\"");
                }
                if (password != null)
                {
                    if(login!=null)
                    {
                        where.Append(" , ");
                    }
                    where.Append("password = \"" + password+"\"");
                }
                if (workerId != -1)
                {
                    if (where.Length > 5)
                    {
                        where.Append(" , ");
                    }
                    where.Append(" workerId = " + workerId);
                }
                return where.ToString();
            }

        }
    }
}
