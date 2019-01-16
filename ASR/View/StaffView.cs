using System;
using System.Collections.Generic;
using System.Text;
using ASR.Controller;
using ASR.Model;

namespace ASR.View
{
    class StaffView
    {
        public void ListStaffView()
        {
            StaffController staffController = new StaffController();
            List<UserModel> staffs = staffController.GetUsers();

            Console.WriteLine("--- List staff ---");
            Console.WriteLine("\tID\tName\tEmail");
            foreach (var staff in staffs)
            {
                Console.WriteLine("\t" + staff.userId + "\t" + staff.name + "\t" + staff.email);
            }
        }
    }
}
