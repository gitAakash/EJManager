using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EJournalManager.Entity;

namespace EJournalManager.Data
{
    public class DbUserRoles : DbHelper
    {
        public int InsertUserRole(UserRole objUserRole)
        {
            int success = 0;
            try
            {
                ObjConnection = new SqlConnection(ConnectionString);
                ObjConnection.Open();
                ObjCommand = new SqlCommand("stp_InsertUserRole", ObjConnection);
                ObjCommand.CommandType = CommandType.StoredProcedure;

                ObjCommand.Parameters.AddWithValue("@Name", objUserRole.Name);
                ObjCommand.Parameters.AddWithValue("@Role_Code", objUserRole.RoleCode);
                ObjCommand.Parameters.AddWithValue("@IsBranchRole", objUserRole.IsBranchRole);
                success = (Int32) ObjCommand.ExecuteScalar();
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
            finally
            {
                if (ObjConnection != null)
                {
                    ObjConnection.Close();
                }
            }

            return success;
        }


        public List<UserRole> GetAllUserRoles()
        {
            var listUserRoleModel = new List<UserRole>();

            try
            {
                ObjConnection = new SqlConnection(ConnectionString);
                ObjConnection.Open();
                ObjCommand = new SqlCommand("stp_GetAllUserRoles", ObjConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                ObjReader = ObjCommand.ExecuteReader();
                if (ObjReader.HasRows)
                {
                    while (ObjReader.Read())
                    {
                        var objUserRole = new UserRole
                        {
                            Id = int.Parse(ObjReader["Id"].ToString()),
                            Name = ObjReader["Name"].ToString(),
                            RoleCode = (ObjReader["Role_Code"]).ToString(),
                            IsBranchRole = (Boolean) ObjReader["IsBranchRole"]
                        };
                        listUserRoleModel.Add(objUserRole);
                    }
                    return listUserRoleModel;
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
            return listUserRoleModel;
        }
    }
}