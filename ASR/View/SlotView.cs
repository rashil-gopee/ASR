using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ASR.Controller;
using ASR.Model;

namespace ASR.View
{
    public class SlotView
    {
        public void CreateSlotView()
        {
            Console.WriteLine("--- Create slot ---");

            RoomController roomController = new RoomController();
            StaffController staffController = new StaffController();

            bool firstRoomTry = true;
            string roomId = null;
            do
            {
                if (!firstRoomTry)
                    Console.WriteLine($"Room {roomId} does not exists. Please try again!");

                firstRoomTry = false;
                Console.WriteLine("Enter room name: ");
                roomId = Console.ReadLine();
            } while (!roomController.CheckIfRoomExists(roomId));


            bool firstDateTry = true;
            string date = null;
            do
            {
                if (!firstDateTry)
                    Console.WriteLine($"{date} is not a valid date. Please try again!");
                firstDateTry = false;

                Console.WriteLine("Enter date for slot (dd-mm-yyyy): ");
                date = Console.ReadLine();
            } while (!Utils.ValidateDate(date));


            bool firstTimeTry = true;
            string time = null;
            do
            {
                if (!firstTimeTry)
                {
                    Console.WriteLine($"{time} is not a valid time. Please try again!");
                    if (Utils.ValidateTime(time) && !time.EndsWith(":00"))
                    {
                        Console.WriteLine("Time must be at the start of the hour.");
                    }
                    if (!(Convert.ToInt32(time.Substring(0, 2)) >= 9 && Convert.ToInt32(time.Substring(0, 2)) <= 14))
                    {
                        Console.WriteLine("Time must be between the school working hours of 9am to 2pm.");
                    }
                }

                firstTimeTry = false;

                Console.WriteLine("Enter time for slot (hh:mm): ");
                time = Console.ReadLine();
            } while (!Utils.ValidateTime(time) || !time.EndsWith(":00") || !(Convert.ToInt32(time.Substring(0, 2)) >= 9 && Convert.ToInt32(time.Substring(0, 2)) <= 14));

            bool firstStaffTry = true;
            string staffId = null;
            do
            {
                if (!firstStaffTry)
                    Console.WriteLine($"{staffId} is not a valid staff. Please try again!");
                firstStaffTry = false;

                Console.WriteLine("Enter staff ID: ");
                staffId = Console.ReadLine();
            } while (!(staffController.CheckIfUserExists(staffId) && staffId.StartsWith('e')));
            
            var room = roomController.GetRoom(roomId);
            var staff = staffController.GetUser(staffId);
            DateTime starTime = DateTime.ParseExact($"{date} {time}:00", "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);

            SlotModel slot = new SlotModel(room, starTime, staff);

            SlotController slotController =  new SlotController();

            if (!slotController.CheckIfSlotExists(slot))
            {
                slotController.CreateSlot(slot);
                Console.WriteLine("Slot has been created successfully.");
            }
            else
            {
                Console.WriteLine($"Slot already exists for {roomId} at {date} {time}");
            }

        }

        public void ListSlotsView()
        {
            Console.WriteLine("--- List slots ---");

            bool firstDateTry = true;
            string date = null;
            do
            {
                if (!firstDateTry)
                    Console.WriteLine($"{date} is not a valid date. Please try again!");
                firstDateTry = false;

                Console.WriteLine("Enter date for slots (dd-mm-yyyy): ");
                date = Console.ReadLine();
            } while (!Utils.ValidateDate(date));

            SlotController slotController = new SlotController();

            List<SlotModel> slots = slotController.GetSlotsByDate(DateTime.ParseExact($"{date} 00:00:00", "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture));

            Console.WriteLine($"Slots on {date}:");
            Console.WriteLine("\tRoom name \tStart time \tStaff ID \tBookings");


            foreach (var slot in slots)
            {
                string time = slot.startTime.ToString("HH:mm");
                string studentId = slot.student == null ? "-" : slot.student.userId;
                Console.WriteLine($"\t{slot.room.RoomId} \t{time} \t{slot.staff.userId} \t{studentId}");
            }

        }

        public void ListStaffAvailability()
        {
            Console.WriteLine("--- List slots ---");

            SlotController slotController = new SlotController();
            StaffController staffController = new StaffController();

            bool firstDateTry = true;
            string date = null;
            do
            {
                if (!firstDateTry)
                    Console.WriteLine($"{date} is not a valid date. Please try again!");
                firstDateTry = false;

                Console.WriteLine("Enter date for staff availability (dd-mm-yyyy): ");
                date = Console.ReadLine();
            } while (!Utils.ValidateDate(date));

            bool firstStaffTry = true;
            string staffId = null;
            do
            {
                if (!firstStaffTry)
                    Console.WriteLine($"{staffId} is not a valid staff. Please try again!");
                firstStaffTry = false;

                Console.WriteLine("Enter staff ID: ");
                staffId = Console.ReadLine();
            } while (!(staffController.CheckIfUserExists(staffId) && staffId.StartsWith('e')));

            var slots = slotController.GetSlotsByDate(DateTime.ParseExact($"{date} 00:00:00", "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture)).Where(x => x.staff.userId.Equals(staffId)).Where(x => x.student == null);

            Console.WriteLine("\tRoom name \tStart time \tStaff ID \tBookings");
            foreach (var slot in slots)
            {
                string time = slot.startTime.ToString("HH:mm");
                string studentId = slot.student == null ? "-" : slot.student.userId;
                Console.WriteLine($"\t{slot.room.RoomId} \t{time} \t{slot.staff.userId} \t{studentId}");
            }
        }

        public void BookSlotView()
        {
            SlotController slotController = new SlotController();

            RoomController roomController = new RoomController();
            StudentController studentController = new StudentController();

            bool firstRoomTry = true;
            string roomId = null;
            do
            {
                if (!firstRoomTry)
                    Console.WriteLine($"Room {roomId} does not exists. Please try again!");

                firstRoomTry = false;
                Console.WriteLine("Enter room name: ");
                roomId = Console.ReadLine();
            } while (!roomController.CheckIfRoomExists(roomId));

            bool firstDateTry = true;
            string date = null;
            do
            {
                if (!firstDateTry)
                    Console.WriteLine($"{date} is not a valid date. Please try again!");
                firstDateTry = false;

                Console.WriteLine("Enter date for slot (dd-mm-yyyy): ");
                date = Console.ReadLine();
            } while (!Utils.ValidateDate(date));

            bool firstTimeTry = true;
            string time = null;
            do
            {
                if (!firstTimeTry)
                    Console.WriteLine($"{time} is not a valid time. Please try again!");
                firstTimeTry = false;

                Console.WriteLine("Enter time for slot (hh:mm): ");
                time = Console.ReadLine();
            } while (!Utils.ValidateTime(time));

            DateTime starTime = DateTime.ParseExact($"{date} {time}:00", "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);

            SlotModel slot = slotController.GetSlot(roomId, starTime);

            if (slot != null)
            {
                if (slot.student == null)
                {
                    bool firstStudentTry = true;
                    string studentId = null;
                    do
                    {
                        if (!firstStudentTry)
                            Console.WriteLine($"{studentId} is not a valid staff. Please try again!");
                        firstStudentTry = false;

                        Console.WriteLine("Enter student ID: ");
                        studentId = Console.ReadLine();
                    } while (!(studentController.CheckIfUserExists(studentId) && studentId.StartsWith('s')));

                    slot.student = studentController.GetUser(studentId);


                    if (slotController.IsBookingAllowed(slot))
                    {
                        if (!slotController.HasExceededStaffBooking(slot))
                        {
                            slotController.BookSlot(slot);
                            Console.WriteLine("Slot has been booked successfully.");
                        }
                        else
                        {
                            Console.WriteLine($"Unable to book! Staff {slot.staff.userId} has already exceeded his booking capacity for {date}");
                        }
                    }
                    else
                        Console.WriteLine($"Unable to book! Student has already booked a slot for {date}");
                }
                else
                {
                    Console.WriteLine("Unable to book! Slot is already booked");
                }
            }
            else
            {
                Console.WriteLine("Unable to book! Slot does not exists!");
            }

        }

        public void DeleteSlotView()
        {
            SlotController slotController = new SlotController();
            RoomController roomController = new RoomController();

            bool firstRoomTry = true;
            string roomId = null;
            do
            {
                if (!firstRoomTry)
                    Console.WriteLine($"Room {roomId} does not exists. Please try again!");

                firstRoomTry = false;
                Console.WriteLine("Enter room name: ");
                roomId = Console.ReadLine();
            } while (!roomController.CheckIfRoomExists(roomId));


            bool firstDateTry = true;
            string date = null;
            do
            {
                if (!firstDateTry)
                    Console.WriteLine($"{date} is not a valid date. Please try again!");
                firstDateTry = false;

                Console.WriteLine("Enter date for slot (dd-mm-yyyy): ");
                date = Console.ReadLine();
            } while (!Utils.ValidateDate(date));


            bool firstTimeTry = true;
            string time = null;
            do
            {
                if (!firstTimeTry)
                    Console.WriteLine($"{time} is not a valid time. Please try again!");
                firstTimeTry = false;

                Console.WriteLine("Enter time for slot (hh:mm): ");
                time = Console.ReadLine();
            } while (!Utils.ValidateTime(time));

            DateTime starTime = DateTime.ParseExact($"{date} {time}:00", "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);

            SlotModel slot = slotController.GetSlot(roomId, starTime);

            if (slot != null)
            {
                if (slot.student == null)
                {
                   slotController.DeleteSlot(slot);
                    Console.WriteLine("Slot has been deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Unable to delete booked slot");
                }
            }
            else
            {
                Console.WriteLine("Slot does not exists!");
            }

        }

        public void CancelSlotBookingView()
        {
            SlotController slotController = new SlotController();
            RoomController roomController = new RoomController();

            bool firstRoomTry = true;
            string roomId = null;
            do
            {
                if (!firstRoomTry)
                    Console.WriteLine($"Room {roomId} does not exists. Please try again!");

                firstRoomTry = false;
                Console.WriteLine("Enter room name: ");
                roomId = Console.ReadLine();
            } while (!roomController.CheckIfRoomExists(roomId));


            bool firstDateTry = true;
            string date = null;
            do
            {
                if (!firstDateTry)
                    Console.WriteLine($"{date} is not a valid date. Please try again!");
                firstDateTry = false;

                Console.WriteLine("Enter date for slot (dd-mm-yyyy): ");
                date = Console.ReadLine();
            } while (!Utils.ValidateDate(date));


            bool firstTimeTry = true;
            string time = null;
            do
            {
                if (!firstTimeTry)
                    Console.WriteLine($"{time} is not a valid time. Please try again!");
                firstTimeTry = false;

                Console.WriteLine("Enter time for slot (hh:mm): ");
                time = Console.ReadLine();
            } while (!Utils.ValidateTime(time));

            DateTime starTime = DateTime.ParseExact($"{date} {time}:00", "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);

            SlotModel slot = slotController.GetSlot(roomId, starTime);

            if (slot.student != null)
            {
                slotController.CancelBooking(slot);
                Console.WriteLine("Slot booking has been cancelled successfully.");
            }
            else
            {
                Console.WriteLine("Slot is not booked!");
            }
        }

        public void RoomAvailabilityView()
        {
            SlotController slotController = new SlotController();
            RoomController roomController = new RoomController();

            bool firstRoomTry = true;
            char roomId = ' ';
            do
            {
                if (!firstRoomTry)
                    Console.WriteLine($"Room {roomId} does not exists. Please try again!");

                firstRoomTry = false;
                Console.WriteLine("Enter room name: ");
                roomId = Console.ReadKey().KeyChar;
                Console.WriteLine();
            } while (!roomController.CheckIfRoomExists(roomId.ToString()));


            bool firstDateTry = true;
            string date = null;
            do
            {
                if (!firstDateTry)
                    Console.WriteLine($"{date} is not a valid date. Please try again!");
                firstDateTry = false;

                Console.WriteLine("Enter date for slot (dd-mm-yyyy): ");
                date = Console.ReadLine();
                
            } while (!Utils.ValidateDate(date));

            DateTime dateTime = DateTime.ParseExact($"{date} 00:00:00", "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);

            var slots = slotController.GetSlotsByDate(dateTime).Where(x => x.room.RoomId.ToString()[0] == roomId);

            Console.WriteLine();
            Console.WriteLine($"\tStart Time\tEnd Time");

            for (int i = 9; i < 13; i++)
            {
                string iStr;
                if (i.ToString().Length == 1)
                {
                    iStr = "0" + i.ToString();
                }
                else
                {
                    iStr = i.ToString();
                }
                DateTime startTime = DateTime.ParseExact($"{date} {iStr}:00:00", "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                DateTime endTime = DateTime.ParseExact($"{date} {Convert.ToInt32(iStr) + 1}:00:00", "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);

                var startTimeStr = startTime.ToString("HH:mm");
                var endTimeStr = endTime.ToString("HH:mm");

                bool exists = false;
                foreach (var slot in slots)
                {
                    string time = slot.startTime.ToString("HH");
                    if (time == iStr)
                        exists = true;
                }

                if (!exists)
                    Console.WriteLine($"\t{startTimeStr}\t{endTimeStr}");
            }
            
        }

    }
}