using System;
using System.Collections.Generic;
using System.Globalization;
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
                    Console.WriteLine($"{time} is not a valid time. Please try again!");
                firstTimeTry = false;

                Console.WriteLine("Enter time for slot (hh:mm): ");
                time = Console.ReadLine();
            } while (!Utils.ValidateTime(time));

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

            List<SlotModel> slots = slotController.getSlotsByDate(DateTime.ParseExact($"{date} 00:00:00", "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture));

            Console.WriteLine($"Slots on {date}:");
            Console.WriteLine("\tRoom name \tStart time \tEnd time \tStaff ID \tBookings");


            foreach (var slot in slots)
            {
                string time = slot.startTime.ToString("HH:mm");
                string studentId = slot.student == null ? "-" : slot.student.userId;
                Console.WriteLine($"\t{slot.room.RoomId} \t{time} \t{slot.staff.userId} \t{studentId}");
            }

        }

    }
}