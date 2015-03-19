using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EJournalManager.Entity;

namespace EJournalManager.Data
{
    public class DbSynchronizer : DbHelper
    {
        public bool GetTask(TaskModel taskModel)
        {
            // bool success = false;
            try
            {
                ObjConnection = new SqlConnection(ConnectionString);
                ObjConnection.Open();
                ObjCommand = new SqlCommand("stp_GetTaskbyId", ObjConnection);
                ObjCommand.CommandType = System.Data.CommandType.StoredProcedure;

                ObjCommand.Parameters.AddWithValue("@TaskId", taskModel.TaskId);
               
                bool success = Convert.ToBoolean(ObjCommand.ExecuteNonQuery());
                if (success)
                    return true;
            }

            catch (Exception ex)
            {
                //Log.Error("Error while Inserting user details into DB : " + ex.Message);
                throw new Exception(ex.Message, ex.InnerException);
            }
            finally
            {
                if (ObjConnection != null)
                {
                    ObjConnection.Close();
                }
            }
            return false;
        }
    }
}
