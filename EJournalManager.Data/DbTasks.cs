using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EJournalManager.Entity;

namespace EJournalManager.Data
{
   public class DbTasks : DbHelper
    {
       public bool InsertTask(TaskModel objTask)
       {
          // bool success = false;
           try
           {
               ObjConnection = new SqlConnection(ConnectionString);
               ObjConnection.Open();
               ObjCommand = new SqlCommand("stp_InsertTask", ObjConnection);
               ObjCommand.CommandType = System.Data.CommandType.StoredProcedure;

               ObjCommand.Parameters.AddWithValue("@Name", objTask.TaskName);
               ObjCommand.Parameters.AddWithValue("@Source", objTask.Source);
               ObjCommand.Parameters.AddWithValue("@Destination", objTask.Destination);
               ObjCommand.Parameters.AddWithValue("@CreatedBy", objTask.CreatedBy);
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

       public List<TaskModel> GetAllTasks()
       {
           var listTaskModel = new List<TaskModel>();

           try
           {
               ObjConnection = new SqlConnection(ConnectionString);
               ObjConnection.Open();
               ObjCommand = new SqlCommand("stp_GetAllTasks", ObjConnection);
               ObjCommand.CommandType = CommandType.StoredProcedure;

               ObjReader = ObjCommand.ExecuteReader();
               if (ObjReader.HasRows)
               {
                   while (ObjReader.Read())
                   {
                       var objTask = new TaskModel
                       {
                           TaskId = Convert.ToInt32(ObjReader["Id"]),
                           TaskName =Convert.ToString(ObjReader["Name"]),
                           Source = Convert.ToString(ObjReader["Source"]),
                           Destination = Convert.ToString(ObjReader["Destination"]),
                       };
                       //ObjBranch.Status = int.Parse(objReader["Status"].ToString());
                       //ObjBranch.RegionId = int.Parse(objReader["RegionId"].ToString());
                       listTaskModel.Add(objTask);
                   }
                   return listTaskModel;
               }
           }
           catch (Exception ex)
           {
               string m = ex.Message;
           }
           finally
           {
               if (ObjConnection != null)
               {
                   ObjConnection.Close();
               }
           }
           return listTaskModel;
       }
    }
}
