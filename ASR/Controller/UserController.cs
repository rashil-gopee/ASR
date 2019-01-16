using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ASR.Model;

namespace ASR.Controller
{
    abstract class UserController
    {
        public abstract UserModel GetUser(string userID);

        public abstract List<UserModel> GetUsers();

        public bool CheckIfUserExists(string userId)
        {
            SqlConnection conn = new SqlConnection(Constants.ConnectionString);
            SqlCommand query = new SqlCommand($"SELECT COUNT(*) FROM [User] WHERE UserID = '{userId}';", conn);
            SqlDataReader read;

            try
            {
                conn.Open();

                read = query.ExecuteReader();

                while (read.Read())
                {
                    return read.GetInt32(0) > 0;
                }

                read.Close();
            }
            catch (SqlException se)
            {
                Console.WriteLine("SQL Exception: {0}", se.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return false;
        }
    }
}
