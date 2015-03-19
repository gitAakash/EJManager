using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EJournalManager.Entity;
using log4net;

namespace EJournalManager.Data
{
    public class DbUsers : DbHelper
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public int InsertUser(UserModel objUser, string CreatedBy)
        {
            int success = 0;
            try
            {
                ObjConnection = new SqlConnection(ConnectionString);
                ObjConnection.Open();
                ObjCommand = new SqlCommand("stp_InsertUser", ObjConnection);
                ObjCommand.CommandType = System.Data.CommandType.StoredProcedure;

                ObjCommand.Parameters.AddWithValue("@FirstName", objUser.FirstName);
                ObjCommand.Parameters.AddWithValue("@LastName", objUser.LastName);
                ObjCommand.Parameters.AddWithValue("@Username", objUser.Username);
                ObjCommand.Parameters.AddWithValue("@Password", objUser.Password);
                ObjCommand.Parameters.AddWithValue("@BranchId", objUser.Branch);
                ObjCommand.Parameters.AddWithValue("@RegionId", objUser.RegionId);
                ObjCommand.Parameters.AddWithValue("@RoleId", objUser.Role);
                ObjCommand.Parameters.AddWithValue("@IsActive", objUser.IsActive);
                ObjCommand.Parameters.AddWithValue("@EmployeeId", objUser.EmployeeId);
                ObjCommand.Parameters.AddWithValue("@Email", objUser.Email);
                ObjCommand.Parameters.AddWithValue("@PhoneNumber", objUser.PhoneNumber);
                ObjCommand.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                ObjCommand.Parameters.AddWithValue("@UserStatus", objUser.UserStatus);
                success = (Int32)ObjCommand.ExecuteScalar();

            }

            catch (Exception ex)
            {
                Log.Error("Error while Inserting user details into DB : " + ex.Message);
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

        public List<VmUserModel> GetAllUsers(string page, string sortdir, string sortColumn, VmUserModel objUserModel)
        {
            var listUserModel = new List<VmUserModel>();
            int iCurrentPage = !string.IsNullOrEmpty(page) ? Convert.ToInt32(page) : 1;
            string strSortCol = !string.IsNullOrEmpty(sortColumn) ? sortColumn + "_" + sortdir : "Fisr";
            string firstName = !string.IsNullOrEmpty(objUserModel.FirstName) ? objUserModel.FirstName.Trim() : "";
            string lastName = !string.IsNullOrEmpty(objUserModel.LastName) ? objUserModel.FirstName.Trim() : "";
            string employeeId = objUserModel.EmployeeId == 0 ? "" : objUserModel.EmployeeId.ToString(CultureInfo.InvariantCulture);
            string email = !string.IsNullOrEmpty(objUserModel.Email) ? objUserModel.Email.Trim() : "";
            string phoneNumber = !string.IsNullOrEmpty(objUserModel.PhoneNumber) ? objUserModel.PhoneNumber.Trim() : "";
            string branchName = !string.IsNullOrEmpty(objUserModel.BranchName) ? objUserModel.BranchName : "";
            string roleId = objUserModel.Role == 0 ? "" : objUserModel.Role.ToString(CultureInfo.InvariantCulture);
            string branchId = objUserModel.Branch == 0 ? "" : objUserModel.Branch.ToString(CultureInfo.InvariantCulture);
            string isActive = "";
            if (objUserModel.IsActive == "2" || objUserModel.IsActive == null)
                isActive = "";
            else
                isActive = objUserModel.IsActive;
            //string IsActive = objUserModel.IsActive == 2 || objUserModel.IsActive == 0 ? "" : objUserModel.IsActive.ToString();

            try
            {
                ObjConnection = new SqlConnection(ConnectionString);
                ObjConnection.Open();
                ObjCommand = new SqlCommand("stp_GetAllUsers", ObjConnection);
                ObjCommand.CommandType = System.Data.CommandType.StoredProcedure;
                ObjCommand.Parameters.AddWithValue("@PageNbr", page);
                ObjCommand.Parameters.AddWithValue("@PageSize", PageSearchSortModel.PageSize);
                ObjCommand.Parameters.AddWithValue("@SortCol", strSortCol);
                ObjCommand.Parameters.AddWithValue("@FirstName", firstName);
                ObjCommand.Parameters.AddWithValue("@LastName", lastName);
                ObjCommand.Parameters.AddWithValue("@EmployeeId", employeeId);
                ObjCommand.Parameters.AddWithValue("@Email", email);
                ObjCommand.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                ObjCommand.Parameters.AddWithValue("@RoleId", roleId);
                ObjCommand.Parameters.AddWithValue("@BranchId", branchId);
                ObjCommand.Parameters.AddWithValue("@IsActive", isActive);


                ObjReader = ObjCommand.ExecuteReader();
                if (ObjReader.HasRows)
                {
                    while (ObjReader.Read())
                    {
                        var objUser = new VmUserModel();
                        objUser.FirstName = ObjReader["FirstName"].ToString();
                        objUser.LastName = ObjReader["LastName"].ToString();
                        objUser.Status = ObjReader["Status"].ToString();
                        objUser.EmployeeId = int.Parse(ObjReader["EmployeeId"].ToString());
                        objUser.Email = ObjReader["Email"].ToString();
                        objUser.PhoneNumber = ObjReader["PhoneNumber"].ToString();
                        objUser.Id = int.Parse(ObjReader["Id"].ToString());
                        objUser.BranchName = ObjReader["BranchName"].ToString();
                        objUser.RoleName = ObjReader["Role"].ToString();
                        objUser.TotalCount = Convert.ToInt32(ObjReader["TotalCount"]);
                        objUser.UserSatus = ObjReader["UserSatus"].ToString();
                        listUserModel.Add(objUser);

                    }
                    return listUserModel;

                }
            }
            catch (Exception ex)
            {
                Log.Error("Error while retriving list of users from DB : " + ex.Message);
                string m = ex.Message;
            }
            finally
            {
                if (ObjConnection != null)
                {
                    ObjConnection.Close();
                }
            }
            return listUserModel;

        }


        public List<VmUserModel> GetAllUsersForExport()
        {
            List<VmUserModel> ListUserModel = new List<VmUserModel>();

            try
            {
                ObjConnection = new SqlConnection(ConnectionString);
                ObjConnection.Open();
                ObjCommand = new SqlCommand("stp_GetAllUsersForExport", ObjConnection);
                ObjCommand.CommandType = System.Data.CommandType.StoredProcedure;

                ObjReader = ObjCommand.ExecuteReader();
                if (ObjReader.HasRows)
                {
                    while (ObjReader.Read())
                    {
                        VmUserModel ObjUser = new VmUserModel();
                        ObjUser.FirstName = ObjReader["FirstName"].ToString();
                        ObjUser.LastName = ObjReader["LastName"].ToString();
                        ObjUser.Status = ObjReader["Status"].ToString();
                        ObjUser.EmployeeId = int.Parse(ObjReader["EmployeeId"].ToString());
                        ObjUser.Email = ObjReader["Email"].ToString();
                        ObjUser.PhoneNumber = ObjReader["PhoneNumber"].ToString();
                        ObjUser.Id = int.Parse(ObjReader["Id"].ToString());
                        ObjUser.BranchName = ObjReader["BranchName"].ToString();
                        ObjUser.RoleName = ObjReader["Role"].ToString();

                        ListUserModel.Add(ObjUser);

                    }
                    return ListUserModel;

                }
            }
            catch (Exception ex)
            {
                Log.Error("Error while retriving list of users from DB fro Export : " + ex.Message);
                string m = ex.Message;
            }
            finally
            {
                if (ObjConnection != null)
                {
                    ObjConnection.Close();
                }
            }
            return ListUserModel;

        }

        public UserModel GetUserByID(string UserID)
        {
            UserModel ObjUser = new UserModel();
            try
            {

                ObjConnection = new SqlConnection(ConnectionString);
                ObjConnection.Open();
                ObjCommand = new SqlCommand("stp_GetUserByID", ObjConnection);
                ObjCommand.CommandType = System.Data.CommandType.StoredProcedure;
                ObjCommand.Parameters.AddWithValue("@UserID", UserID);
                //objCommand.Parameters.AddWithValue("@UserID", AESEncryption.Decrypt(UserID));
                ObjReader = ObjCommand.ExecuteReader();
                if (ObjReader.HasRows)
                {
                    while (ObjReader.Read())
                    {
                        ObjUser.Id = Convert.ToInt32(ObjReader["Id"].ToString());
                        ObjUser.FirstName = ObjReader["FirstName"].ToString();
                        ObjUser.LastName = ObjReader["LastName"].ToString();
                        ObjUser.Username = ObjReader["UserName"].ToString();
                        ObjUser.Password = "********";
                        ObjUser.RoleId = Convert.ToInt32(ObjReader["RoleId"]);
                        ObjUser.BranchId = Convert.ToInt32(ObjReader["BranchId"]);
                        ObjUser.RegionId = Convert.ToInt32(ObjReader["RegionId"]);
                        ObjUser.IsActive = (Boolean)ObjReader["IsActive"];
                        ObjUser.EmployeeId = Convert.ToInt32(ObjReader["EmployeeId"]);
                        ObjUser.Email = ObjReader["Email"].ToString();
                        ObjUser.PhoneNumber = ObjReader["PhoneNumber"].ToString();

                        ObjUser.UserStatus = !(string.IsNullOrEmpty(ObjReader["UserStatus"].ToString())) ? Convert.ToInt32(ObjReader["UserStatus"].ToString()) : 0;
                    }

                }
            }
            catch (Exception ex)
            {
                Log.Error("Error while retriving user details by UserId from DB : " + ex.Message);
                throw new Exception(ex.Message, ex.InnerException);
            }
            finally
            {
                if (ObjConnection != null)
                {
                    ObjConnection.Close();
                }
            }

            return ObjUser;
        }

        public int UpdateUser(UserModel ObjUser, string UpdatedBy)
        {
            int Success = 0;

            try
            {
                ObjConnection = new SqlConnection(ConnectionString);
                ObjConnection.Open();
                ObjCommand = new SqlCommand("stp_UpdateUser", ObjConnection);
                ObjCommand.CommandType = System.Data.CommandType.StoredProcedure;
                ObjCommand.Parameters.AddWithValue("@Id", ObjUser.Id);
                ObjCommand.Parameters.AddWithValue("@FirstName", ObjUser.FirstName);
                ObjCommand.Parameters.AddWithValue("@LastName", ObjUser.LastName);
                //objCommand.Parameters.AddWithValue("@Username", ObjUser.Username);
                //objCommand.Parameters.AddWithValue("@Password", ObjUser.Password);
                ObjCommand.Parameters.AddWithValue("@BranchId", ObjUser.BranchId);
                ObjCommand.Parameters.AddWithValue("@RoleId", ObjUser.RoleId);
                ObjCommand.Parameters.AddWithValue("@RegionId", ObjUser.RegionId);
                ObjCommand.Parameters.AddWithValue("@IsActive", ObjUser.IsActive);
                //objCommand.Parameters.AddWithValue("@EmployeeId", ObjUser.EmployeeId);
                ObjCommand.Parameters.AddWithValue("@Email", ObjUser.Email);
                ObjCommand.Parameters.AddWithValue("@PhoneNumber", ObjUser.PhoneNumber);
                ObjCommand.Parameters.AddWithValue("@ModifiedBy", UpdatedBy);
                ObjCommand.Parameters.AddWithValue("@UserStatus", ObjUser.UserStatus);

                Success = ObjCommand.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                Log.Error("Error while Updating user details into DB : " + ex.Message);
                throw new Exception(ex.Message, ex.InnerException);
            }
            finally
            {
                if (ObjConnection != null)
                {
                    ObjConnection.Close();
                }
            }
            return Success;
        }


        public int InsertEscalation(UserModel objUser)
        {
            int success = 0;
            try
            {
                ObjConnection = new SqlConnection(ConnectionString);
                ObjConnection.Open();
                ObjCommand = new SqlCommand("stp_InsertEscalationDetails", ObjConnection);
                ObjCommand.CommandType = System.Data.CommandType.StoredProcedure;
                ObjCommand.Parameters.AddWithValue("@Name", objUser.FirstName + " " + objUser.LastName);
                ObjCommand.Parameters.AddWithValue("@BranchId", objUser.Branch);
                ObjCommand.Parameters.AddWithValue("@RoleId", objUser.Role);
                ObjCommand.Parameters.AddWithValue("@RegionId", objUser.Region);
                ObjCommand.Parameters.AddWithValue("@Email", objUser.Email);
                ObjCommand.Parameters.AddWithValue("@PhoneNumber", objUser.PhoneNumber);
                ObjCommand.Parameters.AddWithValue("@TerminalId", "");
                success = (Int32)ObjCommand.ExecuteScalar();

            }

            catch (Exception ex)
            {
                Log.Error("Error while inserting Esaclation details into DB : " + ex.Message);
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


        public RegionModel GetRegionName(string branchName)
        {
            // string region = string.Empty;
            RegionModel objRegion = new RegionModel();
            try
            {
                ObjConnection = new SqlConnection(ConnectionString);
                ObjConnection.Open();
                ObjCommand = new SqlCommand("stp_GetRegionNameforBranch", ObjConnection);
                ObjCommand.CommandType = System.Data.CommandType.StoredProcedure;
                ObjCommand.Parameters.AddWithValue("@BranchName", branchName);

                ObjReader = ObjCommand.ExecuteReader();
                if (ObjReader.HasRows)
                {
                    while (ObjReader.Read())
                    {
                        objRegion.Id = Convert.ToInt32(ObjReader["Id"].ToString());
                        objRegion.Name = ObjReader["Name"].ToString();
                    }

                }

            }
            catch (Exception ex)
            {
                Log.Error("Error while Retriving Region name on selection of branch name : " + ex.Message);
                throw new Exception(ex.Message, ex.InnerException);
            }
            finally
            {
                if (ObjConnection != null)
                {
                    ObjConnection.Close();
                }
            }

            return objRegion;
        }

        public int UpdateUserPassword(UserModel ObjUser, string UpdatedBy)
        {
            int success = 0;

            try
            {
                ObjConnection = new SqlConnection(ConnectionString);
                ObjConnection.Open();
                ObjCommand = new SqlCommand("stp_UpdateUserPassword", ObjConnection);
                ObjCommand.CommandType = System.Data.CommandType.StoredProcedure;
                ObjCommand.Parameters.AddWithValue("@Id", ObjUser.Id);
                ObjCommand.Parameters.AddWithValue("@Password", ObjUser.Password);
                ObjCommand.Parameters.AddWithValue("@ModifiedBy", UpdatedBy);
                success = ObjCommand.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                Log.Error("Error while Updating user password in DB : " + ex.Message);
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
    }
}
