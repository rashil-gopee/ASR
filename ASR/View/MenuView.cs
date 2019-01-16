using System;
using System.Collections.Generic;
using System.Text;

namespace ASR.View
{
    class MenuView
    {
        public static void DisplayWelcomeView()
        {
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("Welcome to Appointment Scheduling and Reservation System");
            Console.WriteLine("------------------------------------------------------------");
            
            DisplayMainMenu();
        }

        public static void DisplayMainMenu()
        {
            Console.WriteLine();
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("Main menu: \t 1. List rooms \t 2. List slots \t 3. Staff Menu \t 4.Student menu \t 5. Exit");
            Console.WriteLine();

            var input = Console.ReadLine();
            if (int.TryParse(input, out int number))
            {
                if (number == 1)
                {
                    RoomView roomView = new RoomView();
                    roomView.GetRoomsView();
                }
                else if (number == 2)
                {
                    SlotView slotView = new SlotView();
                    slotView.ListSlotsView();
                    DisplayMainMenu();
                }
                else if (number == 3)
                {
                    DisplayStaffMenu();
                }
                else if (number == 4)
                {
                    DisplayStudentMenu();
                }
                else if (number == 5)
                {
                    Environment.Exit(0);
                }
            }
            else
            {
                Console.WriteLine($"{input} is not a valid option");
                DisplayMainMenu();
            }

        }

        public static void DisplayStaffMenu()
        {
            Console.WriteLine();
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("Staff menu: \t 1. List staffs \t 2. Room availability \t 3. Create slot \t 4. Remove slot \t 5. Exit");
            Console.WriteLine();

            var input = Console.ReadLine();
            if (int.TryParse(input, out int number))
            {
                if (number == 1)
                {
                    StaffView staffView = new StaffView();
                    staffView.ListStaffView();
                    DisplayStaffMenu();
                }
                else if (number == 2)
                {

                }
                else if (number == 3)
                {
                    SlotView slotView = new SlotView();
                    slotView.CreateSlotView();
                    DisplayStaffMenu();
                }
                else if (number == 5)
                {
                    DisplayMainMenu();
                }
            }
            else
            {
                Console.WriteLine($"{input} is not a valid option");
                DisplayStaffMenu();
            }
        }

        public static void DisplayStudentMenu()
        {
            Console.WriteLine();
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("Staff menu: \t 1. List students \t 2. Staff availability \t 3. Make booking \t 4. Cancel booking \t 5. Exit");
            Console.WriteLine();

            var input = Console.ReadLine();
            if (int.TryParse(input, out int number))
            {
                if (number == 1)
                {
                    StudentView studentView = new StudentView();
                    studentView.ListStudentsView();
                    DisplayStudentMenu();
                }
                else if (number == 2)
                {

                }
                else if (number == 5)
                {
                    DisplayMainMenu();
                }
            }
            else
            {
                Console.WriteLine($"{input} is not a valid option");
                DisplayStudentMenu();
            }
        }

    }
}
