using System;
using System.Data;
using System.Data.SqlClient;
using EJournalManager.Entity;

namespace EJournalManager.Data
{
    public class DbCustRoleProvider : DbHelper
    {
        private SqlCommand _objCommand;
        private SqlConnection _objConnection;
        private SqlDataReader _objReader;

        public string GetUserRolesByUserId(string userName)
        {
            string userRole = null;
            try
            {
                _objConnection = new SqlConnection(ConnectionString);
                _objConnection.Open();

                _objCommand = new SqlCommand("stp_GetUserRoleByUsername", _objConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                _objCommand.Parameters.AddWithValue("@UserName", userName);
                _objReader = _objCommand.ExecuteReader();

                while (_objReader.Read())
                {
                    userRole = _objReader["Role"].ToString();
                }

                if (_objReader != null)
                {
                    _objReader.Close();
                }
            }
            catch (Exception e)
            {
                //Write a code which will logged an error if any.
            }
            return userRole;
        }

        public UserRole GetRoleById(int roleId)
        {
            var userRole = new UserRole();
            try
            {
                _objConnection = new SqlConnection(ConnectionString);
                _objConnection.Open();

                _objCommand = new SqlCommand("stp_GetRoleByID", _objConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                _objCommand.Parameters.AddWithValue("@RoleIdID", roleId);
                _objReader = _objCommand.ExecuteReader();

                while (_objReader.Read())
                {
                    userRole.Id = Convert.ToInt32(_objReader["Id"].ToString());
                    userRole.Name = (_objReader["Name"].ToString());
                    userRole.RoleCode = (_objReader["Role_Code"].ToString());
                    userRole.IsBranchRole = Convert.ToBoolean(_objReader["IsBranchRole"].ToString());
                }

                if (_objReader != null)
                {
                    _objReader.Close();
                }
            }
            catch (Exception e)
            {
                //Write a code which will logged an error if any.
            }
            return userRole;
        }

        public UserRole GetUserRole(int userId)
        {
            var objUserRole = new UserRole();
            try
            {
                _objConnection = new SqlConnection(ConnectionString);
                _objConnection.Open();

                _objCommand = new SqlCommand("stp_GetUserRoleByUserId", _objConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                _objCommand.Parameters.AddWithValue("@UserId", userId);
                _objReader = _objCommand.ExecuteReader();

                while (_objReader.Read())
                {
                    objUserRole.Name = _objReader["Name"].ToString();
                    objUserRole.IsBranchRole = Boolean.Parse(_objReader["IsBranchRole"].ToString());
                }

                if (_objReader != null)
                {
                    _objReader.Close();
                }
            }
            catch (Exception e)
            {
                //Write a code which will logged an error if any.
            }
            return objUserRole;
        }
    }
}