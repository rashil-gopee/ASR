using System;

namespace ASR.Model
{
    public class SlotModel
    {
        private RoomModel room { get; set; }
        private DateTime startTime { get; set; }
        private UserModel staff { get; set; }
        private UserModel student { get; set; }

        public SlotModel(RoomModel room, DateTime startTime, UserModel staff)
        {
            this.room = room;
            this.startTime = startTime;
            this.staff = staff;
        }

        public SlotModel(RoomModel room, DateTime startTime, UserModel staff, UserModel student)
        {
            this.room = room;
            this.startTime = startTime;
            this.staff = staff;
            this.student = student;
        }

    }
}