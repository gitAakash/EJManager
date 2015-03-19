using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EJournalManager.Entity;

namespace EJournalManager.Data
{
    public class DbBranch : DbHelper
    {
     
        /// <summary>
        ///     Get List of all Branches from the Branch table
        /// </summary>
        /// <returns></returns>
        public List<BranchModel> GetAllBranches()
        {
            var listBranchModel = new List<BranchModel>();

            try
            {
                ObjConnection = new SqlConnection(ConnectionString);
                ObjConnection.Open();
                ObjCommand = new SqlCommand("stp_GetAllBranches", ObjConnection);
                ObjCommand.CommandType = CommandType.StoredProcedure;

                ObjReader = ObjCommand.ExecuteReader();
                if (ObjReader.HasRows)
                {
                    while (ObjReader.Read())
                    {
                        var objBranch = new BranchModel
                        {
                            Id = int.Parse(ObjReader["Id"].ToString()),
                            Name = ObjReader["Name"].ToString(),
                            Code = (ObjReader["Code"]).ToString(),
                            IsDeleted = (Boolean) ObjReader["IsDeleted"],
                            Address = ObjReader["Address"].ToString()
                        };
                        //ObjBranch.Status = int.Parse(objReader["Status"].ToString());
                        //ObjBranch.RegionId = int.Parse(objReader["RegionId"].ToString());
                        listBranchModel.Add(objBranch);
                    }
                    return listBranchModel;
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
            return listBranchModel;
        }


        /// <summary>
        ///     Check if user exist for that particular branch
        /// </summary>
        /// <param name="RegionId"></param>
        /// <param name="branchId"></param>
        /// <returns></returns>
        public int CheckIfUserExistforBranch(int branchId)
        {
            int iuserexist = 0;

            try
            {
                ObjConnection = new SqlConnection(ConnectionString);
                ObjConnection.Open();
                ObjCommand = new SqlCommand("stp_CheckIfUserExistforBranch", ObjConnection);
                ObjCommand.Parameters.Add("@BranchId", branchId);
                ObjCommand.CommandType = CommandType.StoredProcedure;
                iuserexist = (int) ObjCommand.ExecuteScalar();
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
            return iuserexist;
        }
    }
}