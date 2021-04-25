using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace DL.Repositories
{
    public class WorkerEntityRepo : Abstract.IWorkerEntityRepo
    {
        private MySqlConnection connection;
        private string addString = "INSERT INTO worker (PassportNumber, PersonalData) values (@passport_n, @passport_d);SELECT LAST_INSERT_ID();";
        private string deleteString = "Delete from worker where PassportNumber=@passportNumber; ";
        private string readString = "select * from worker ";
        private string updateString = "update worker ";
        public WorkerEntityRepo(string connectionString = @"Server=localhost;Port=3306;Database=work_fac;Uid=ForSomeCase;password=Kukrakuska713")  
        {
            connection = new MySqlConnection(connectionString);
        }
        public void Create(WorkerEntity worker)
        {
            connection.Open();
            MySqlCommand command = new MySqlCommand(addString);

            MySqlParameter numberParam = new MySqlParameter("@passport_n", worker.PassportNumber);
            MySqlParameter dataParam = new MySqlParameter("@passport_d", worker.PersonalData);
            
            command.Parameters.Add(numberParam);
            command.Parameters.Add(dataParam);
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
                worker.PassportNumber = id;
            }
            else
            {
                throw new ArgumentException("error of creating");
            }
        }

        public void Delete(WorkerEntity worker)
        {
            connection.Open();
            MySqlCommand command = new MySqlCommand(deleteString);
            MySqlParameter parameter = new MySqlParameter("@passportNumber", worker.PassportNumber.ToString());
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

        public List<WorkerEntity> Read(int minPassportNumber=-1, int maxPassportNumber=-1, string PersonalData=null)
        {
            string stringWithWhere = null;
            try
            {
                stringWithWhere = CreateWherePartForReadQuery(minPassportNumber, maxPassportNumber, PersonalData);
            }
            catch (Exception)
            {
                throw;
            }
            MySqlCommand command= new MySqlCommand(readString+ stringWithWhere);
            command.Connection = connection;
            
            connection.Open();
            MySqlDataReader reader =  command.ExecuteReader();
            List<WorkerEntity> result = new List<WorkerEntity>();
            while (reader.Read())
            {
                object passportNumber = reader["PassportNumber"];
                object personalDataFromDb = reader["PersonalData"];
                WorkerEntity worker = new WorkerEntity
                {
                    PassportNumber = System.Convert.ToInt32(passportNumber),
                    PersonalData = System.Convert.ToString(personalDataFromDb),
                };
                result.Add(worker);
            }
            connection.Close();
            
            return result;
        }

        public void Update(WorkerEntity worker, string PersonalData = null)
        {
            connection.Open();
            
            string setString = CreateSetPartForUpdateQuery(PersonalData);
            
            MySqlCommand command = new MySqlCommand(updateString + setString + $" where PassportNumber = {worker.PassportNumber};");

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

        private string CreateWherePartForReadQuery(int minPassportNumber , int maxPassportNumber , string personalData)
        {
            if (minPassportNumber > maxPassportNumber)
            {
                throw new ArgumentException("Wrong id params");
            }
            StringBuilder query;
            if(minPassportNumber!=-1 || maxPassportNumber!= -1 || personalData!=null)
            {
                query = new StringBuilder();
                query.Append("where ");

                if (maxPassportNumber != minPassportNumber)
                {
                    if (minPassportNumber != -1)
                    {
                        query.Append(" PassportNumber>" + minPassportNumber.ToString());
                    }
                    if (maxPassportNumber != -1)
                    {
                        if(minPassportNumber != -1)
                        {
                            query.Append(" and ");
                        }
                        query.Append(" PassportNumber<" + maxPassportNumber.ToString());
                    }
                }
                else
                {
                    if (maxPassportNumber != -1)
                    {
                        query.Append(" PassportNumber = " + maxPassportNumber.ToString());
                    }
                }
                if (personalData != null)
                {
                    if (query.Length > 6)
                    {
                        query.Append(" and ");
                    }
                    query.Append(" PersonalData = \"" + personalData + "\"");
                }
                return query.ToString();
            }
            else
            {
                return null;
            }
        }
        private string CreateSetPartForUpdateQuery(string personalData)
        {
            if(personalData==null)
            {
                return null;
            }
            else
            {
                StringBuilder where = new StringBuilder();
                where.Append(" set ");
                if (personalData != null)
                {
                    where.Append("PersonalData = \"" + personalData+"\"");
                }
                return where.ToString();
            }
        }
    }

   


}
