using System;
using ASR.Controller;

namespace ASR.View
{
    public class RoomView
    {
        public void GetRoomsView()
        {
            RoomController roomController = new RoomController();

            var rooms = roomController.GetRooms();

            Console.WriteLine("--- List rooms ---");
            Console.WriteLine("\tRoom Name");
            foreach (var room in rooms)
            {
                Console.WriteLine("\t" + room.RoomId);
            }
        }
    }
}