using System;
using System.Data;
using System.Data.SqlClient;
using EJournalManager.Entity;

namespace EJournalManager.Data
{
    public class DbCustMembership : DbHelper
    {
        private SqlCommand _objCommand;
        private SqlConnection _objConnection;
        private SqlDataReader _objReader;

        public User GetUserIdbyNameAndPassword(string strUName, string strPassword)
        {
            User objuser = null;
            try
            {
                _objConnection = new SqlConnection(ConnectionString);
                _objConnection.Open();
                _objCommand = new SqlCommand("stp_ValidateUser", _objConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                _objCommand.Parameters.AddWithValue("@Username", strUName);
                _objCommand.Parameters.AddWithValue("@Password", strPassword);
                _objReader = _objCommand.ExecuteReader();

                if (_objReader.Read())
                {
                    objuser = new User
                    {
                        UserId = Convert.ToInt32(_objReader["Id"].ToString()),
                        UserName = _objReader["Username"].ToString(),
                        FirstName = _objReader["FirstName"].ToString(),
                        LastName = _objReader["LastName"].ToString(),
                        RoleId = Convert.ToInt32(_objReader["RoleId"].ToString())
                    };
                }

                if (_objReader != null)
                {
                    _objReader.Close();
                }

                return objuser;
            }
            catch (Exception ex)
            {
                string m = ex.Message;
            }
            finally
            {
                if (_objConnection != null)
                {
                    _objConnection.Close();
                }
            }
            return objuser;
        }

        public bool ChangePassword(string username, string oldpassword, string newpassword)
        {
            try
            {
                _objConnection = new SqlConnection(ConnectionString);
                _objConnection.Open();
                _objCommand = new SqlCommand("stp_ChnagePassword", _objConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                _objCommand.Parameters.AddWithValue("@username", username);
                _objCommand.Parameters.AddWithValue("@password", oldpassword);
                _objCommand.Parameters.AddWithValue("@newpassword", newpassword);
                var success = (int) _objCommand.ExecuteScalar();
                if (success > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                string m = ex.Message;
            }
            finally
            {
                if (_objConnection != null)
                {
                    _objConnection.Close();
                }
            }
            return false;
        }
    }
}