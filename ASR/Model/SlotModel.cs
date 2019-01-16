using System;

namespace ASR.Model
{
    public class SlotModel
    {
        public RoomModel room { get; set; }
        public DateTime startTime { get; set; }
        public UserModel staff { get; set; }
        public UserModel student { get; set; }

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