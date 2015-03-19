using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EJournalManager.Data
{
    public class DbHelper
    {
        private SqlConnection _dbAtMonitor;

        public DbHelper()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["EJournalConnectionString"].ConnectionString;
            ObjConnection = DbConection;
            ObjCommand = null;
            ObjReader = null;
        }

        public string ConnectionString { get; set; }

        public SqlConnection ObjConnection { get; set; }
        public SqlCommand ObjCommand { get; set; }
        public SqlDataReader ObjReader { get; set; }

        public SqlConnection DbConection
        {
            get
            {
                if (_dbAtMonitor == null)
                {
                    _dbAtMonitor = new SqlConnection(ConnectionString);
                }
                return _dbAtMonitor;
            }
        }

        public void OpenConnection()
        {
            ObjConnection = ObjConnection;
            if (ObjConnection.State != ConnectionState.Open)
                ObjConnection.Open();
        }
    }
}