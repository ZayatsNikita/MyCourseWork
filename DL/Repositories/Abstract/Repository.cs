using System.Data.SqlClient;

namespace DL.Repositories.Abstract
{
    public abstract class Repository
    {
        public Repository(string connectionString)
        {
            this.connection = new SqlConnection(connectionString);
        } 

        protected SqlConnection connection;
    }
}
