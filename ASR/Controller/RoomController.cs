using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ASR.Model;

namespace ASR.Controller
{
    public class RoomController
    {
        public List<RoomModel> GetRooms()
        {
            List<RoomModel> rooms = new List<RoomModel>();

            SqlConnection conn = new SqlConnection(Constants.CONNECTION);
            SqlCommand query = new SqlCommand("select * from Room", conn);
            SqlDataReader read;

            try
            {
                conn.Open();

                read = query.ExecuteReader();

                while (read.Read())
                {
                    rooms.Add(new RoomModel(read["RoomID"].ToString()));
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

            return rooms;
        }
    }
}