using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EJournalManager.Entity;

namespace EJournalManager.Data
{
    public class DbRegion : DbHelper
    {
        public List<RegionModel> GetAllRegions()
        {
            var listRegionModel = new List<RegionModel>();

            try
            {
                ObjConnection = new SqlConnection(ConnectionString);
                ObjConnection.Open();
                ObjCommand = new SqlCommand("stp_GetAllRegions", ObjConnection);
                ObjCommand.CommandType = CommandType.StoredProcedure;

                ObjReader = ObjCommand.ExecuteReader();
                if (ObjReader.HasRows)
                {
                    while (ObjReader.Read())
                    {
                        var objRegion = new RegionModel();
                        objRegion.Id = int.Parse(ObjReader["Id"].ToString());
                        objRegion.Name = ObjReader["Name"].ToString();
                        objRegion.Code = (ObjReader["Code"]).ToString();
                        objRegion.IsDeleted = (Boolean) ObjReader["IsDeleted"];
                        listRegionModel.Add(objRegion);
                    }
                    return listRegionModel;
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
            return listRegionModel;
        }


        /// <summary>
        ///     Check if user exist for that particular region
        /// </summary>
        /// <param name="RegionId"></param>
        /// <returns></returns>
        public int CheckIfUserExistforRegion(int RegionId)
        {
            int iuserexist = 0;

            try
            {
                ObjConnection = new SqlConnection(ConnectionString);
                ObjConnection.Open();
                ObjCommand = new SqlCommand("stp_CheckIfUserExistforRegion", ObjConnection);
                ObjCommand.Parameters.Add("@RegionId", RegionId);
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