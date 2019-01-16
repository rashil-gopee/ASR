using System;
using System.Collections.Generic;
using System.Text;
using ASR.Controller;
using ASR.Model;

namespace ASR.View
{
    class StudentView
    {
        public void ListStudentsView()
        {
            StudentController studentController = new StudentController();
            List<UserModel> students = studentController.GetUsers();

            Console.WriteLine("--- List student ---");
            Console.WriteLine("\tID\tName\tEmail");
            foreach (var student in students)
            {
                Console.WriteLine("\t" + student.userId + "\t" + student.name + "\t" + student.email);
            }
        }
    }
}
