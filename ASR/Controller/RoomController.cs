using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ASR.Model;

namespace ASR.Controller
{
    public class RoomController
    {
        public RoomModel GetRoom(string roomId)
        {
            RoomModel room = null;

            SqlConnection conn = new SqlConnection(Constants.ConnectionString);
            SqlCommand query = new SqlCommand($"SELECT * from Room WHERE RoomID = '{roomId}';", conn);
            SqlDataReader read;

            try
            {
                conn.Open();

                read = query.ExecuteReader();

                while (read.Read())
                {
                    room = new RoomModel(read["RoomID"].ToString());
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

            return room;
        }
        public List<RoomModel> GetRooms()
        {
            List<RoomModel> rooms = new List<RoomModel>();

            SqlConnection conn = new SqlConnection(Constants.ConnectionString);
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

        public bool CheckIfRoomExists(string roomId)
        {
            SqlConnection conn = new SqlConnection(Constants.ConnectionString);
            SqlCommand query = new SqlCommand($"SELECT COUNT(*) FROM Room WHERE RoomID = '{roomId}';", conn);
            SqlDataReader read;

            try
            {
                conn.Open();

                read = query.ExecuteReader();

                while (read.Read())
                {
                    if (read.GetInt32(0) > 0)
                        return true;
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