using System;

namespace ASR.Model
{
    public class RoomModel
    {
        public Char RoomId { get; set; }

        public RoomModel(Char RoomId)
        {
            this.RoomId = RoomId;
        }
    }
}