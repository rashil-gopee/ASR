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

        }

        public List<SlotModel> getSlotsByDate(DateTime date)
        {
            UserModel personModel;
            List<SlotModel> slots = new List<SlotModel>();

            SqlConnection conn = new SqlConnection(Constants.CONNECTION);
            SqlCommand query = new SqlCommand("select * from Slot", conn);
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

                    staffModel = staffController.GetUser(read["UserID"].ToString());

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

                    read.Close();
                }
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

        public void BookSlot(SlotModel slot, StudentModel student)
        {

        }
    }
}