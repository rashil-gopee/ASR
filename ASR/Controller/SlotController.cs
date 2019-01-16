using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ASR.Model;

namespace ASR.Controller
{
    public class SlotController
    {
        public void CreateSlot(SlotModel slot)
        {
            using (SqlConnection conn = new SqlConnection(Constants.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    
                    cmd.CommandText = @"INSERT INTO Slot(RoomID,StartTime,StaffID) 
                            VALUES(@RoomID,@StartTime,@StaffID)";

                    cmd.Parameters.AddWithValue("@RoomID", slot.room.RoomId);
                    cmd.Parameters.AddWithValue("@StartTime", SqlDbType.DateTime2).Value = slot.startTime;
                    cmd.Parameters.AddWithValue("@StaffID", slot.staff.userId);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException e)
                    {
                        Console.WriteLine("SQL Exception: {0}", e.Message);
                    }

                }
            }
        }

        public List<SlotModel> getSlotsByDate(DateTime date)
        {
            UserModel personModel;
            List<SlotModel> slots = new List<SlotModel>();

            SqlConnection conn = new SqlConnection(Constants.ConnectionString);

            var dateStr = date.ToString("yyyy-MM-dd");

            SqlCommand query = new SqlCommand($"select * from Slot WHERE StartTime >= '{dateStr} 00:00:00' AND StartTime <= '{dateStr} 23:59:59'", conn);
            SqlDataReader read;

            try
            {
                conn.Open();

                read = query.ExecuteReader();

                while (read.Read())
                {
                    StaffController staffController;
                    UserModel staffModel;
                    
                    staffController = new StaffController();

                    staffModel = staffController.GetUser(read["StaffID"].ToString());

                    if (!String.IsNullOrWhiteSpace(read["BookedInStudentID"].ToString()))
                    {
                        UserModel studentModel;
                        StudentController studentController = new StudentController();
                        studentModel = studentController.GetUser(read["BookedInStudentID"].ToString());
                        
                        slots.Add(new SlotModel(new RoomModel(read["RoomID"].ToString()), Convert.ToDateTime(read["StartTime"]), staffModel, studentModel));
                    }
                    else
                    {
                        slots.Add(new SlotModel(new RoomModel(read["RoomID"].ToString()), Convert.ToDateTime(read["StartTime"]), staffModel));
                    }
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

            return slots;
        }

        public bool CheckIfSlotExists(SlotModel slot)
        {
            SqlConnection conn = new SqlConnection(Constants.ConnectionString);
            SqlCommand query = new SqlCommand($"SELECT COUNT(*) FROM Slot WHERE RoomID = '{slot.room.RoomId}' AND StartTime < '{slot.startTime}';", conn);

            //using (SqlCommand cmd = new SqlCommand(query, conn))
            //{
            //    cmd.Parameters.AddWithValue("StartTime", slot.startTime + "00:00:00");
            //    cmd.Parameters.AddWithValue("date2", dateAdded2 + "23:59:59");
            //    SqlDataAdapter da = new SqlDataAdapter(cmd);
            //    da.Fill(holder);
            //}

            SqlDataReader read;

            try
            {
                conn.Open();

                read = query.ExecuteReader();

                while (read.Read())
                {
                    Console.WriteLine(read.GetValue(0));
                    return true;
                    //if (Int32.Parse(read.GetString(0)) > 0)
                    //    return true;
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

        public void BookSlot(SlotModel slot)
        {
            StudentController studentController = new StudentController();
            
            using (SqlConnection conn = new SqlConnection(Constants.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    
                    cmd.CommandText = @"UPDATE Slot WHERE RoomID = @RoomID, StartTime = @StartTime SET BookedInStudentID = @BookedInStudentID;";

                    cmd.Parameters.AddWithValue("@RoomID", slot.room.RoomId);
                    cmd.Parameters.AddWithValue("@StartTime", slot.startTime);
                    cmd.Parameters.AddWithValue("@BookedInStudentID", slot.student.userId);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException e)
                    {
                        Console.WriteLine("SQL Exception: {0}", e.Message);
                    }

                }
            }
        }


        public void CancelBooking(SlotModel slot)
        {
            using (SqlConnection conn = new SqlConnection(Constants.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;

                    //if (slot.student != null)
                    cmd.CommandText = @"UPDATE Slot WHERE RoomID = @RoomID, StartTime = @StartTime SET BookedInStudentID = Null";

                    cmd.Parameters.AddWithValue("@RoomID", slot.room.RoomId);
                    cmd.Parameters.AddWithValue("@StartTime", slot.startTime);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException e)
                    {
                        Console.WriteLine("SQL Exception: {0}", e.Message);
                    }

                }
            }
        }

    }
}