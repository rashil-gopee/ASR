using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using ASR.Model;

namespace ASR.Controller
{
    class StaffController: UserController
    {
        public override UserModel GetUser(string userID)
        {
            UserModel personModel = null;
            SqlConnection conn = new SqlConnection(Constants.CONNECTION);
            SqlCommand query = new SqlCommand(String.Format("select from Room where UserID = '{0}'", userID), conn);
            SqlDataReader read;

            try
            {
                conn.Open();

                read = query.ExecuteReader();

                while (read.Read())
                {
                    if (read["UserID"].ToString().StartsWith('e'))
                        personModel = new StaffModel(read["UserID"].ToString(), read["Name"].ToString(),
                        read["Email"].ToString());
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

            return personModel;
        }

        public override List<UserModel> GetUsers()
        {
            List<UserModel> staffs = new List<UserModel>();

            SqlConnection conn = new SqlConnection(Constants.CONNECTION);
            SqlCommand query = new SqlCommand("SELECT * FROM [User] WHERE name LIKE 'e%';", conn);
            SqlDataReader read;

            try
            {
                conn.Open();

                read = query.ExecuteReader();

                while (read.Read())
                {
                    staffs.Add(new StaffModel(read["UserID"].ToString(), read["Name"].ToString(),
                        read["Email"].ToString()));
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

            return staffs;
        }

    }

}
