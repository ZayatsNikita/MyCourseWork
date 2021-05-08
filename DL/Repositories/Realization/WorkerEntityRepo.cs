using DL.Entities;
using DL.Extensions;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
namespace DL.Repositories
{
    public class WorkerEntityRepo : Abstract.IWorkerEntityRepo
    {
        private MySqlConnection connection;
        private string addString = "INSERT INTO worker (PassportNumber, PersonalData) values (@passport_n, @passport_d);SELECT LAST_INSERT_ID();";
        private string deleteString = "Delete from worker where PassportNumber=@passportNumber; ";
        private string readString = "select * from worker ";
        private string updateString = "update worker ";
        public WorkerEntityRepo(string connectionString)  
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
            finally
            {
                connection.Close();
            }
            int id = Convert.ToInt32(obj);
            worker.PassportNumber = id;
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
            finally
            {
                connection.Close();
            }
        }

        public List<WorkerEntity> Read(int minPassportNumber=-1, int maxPassportNumber=-1, string PersonalData=null)
        {
            string stringWithWhere = CreateWherePartForReadQuery(minPassportNumber, maxPassportNumber, PersonalData);

            MySqlCommand command= new MySqlCommand(readString+ stringWithWhere);
            
            command.Connection = connection;
            
            connection.Open();
            
            List<WorkerEntity> result = new List<WorkerEntity>();

            try
            {
                MySqlDataReader reader = command.ExecuteReader();
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
            }
            finally
            {
                connection.Close();
            }
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
            finally
            {
                connection.Close();
            }
        }

        private string CreateWherePartForReadQuery(int minPassportNumber , int maxPassportNumber , string personalData)
        {
            if(minPassportNumber!=-1 || maxPassportNumber!= -1 || personalData!=null)
            {
                StringBuilder query = new StringBuilder();

                query.AddWhereWord();

                query.AddWhereParam(minPassportNumber, maxPassportNumber, "PassportNumber");

                query.AddWhereParam(personalData, "PersonalData");

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
                where.AddSetWord();

                where.AddSetParam(personalData, "PersonalData");
                
                return where.ToString();
            }
        }
    }
}
