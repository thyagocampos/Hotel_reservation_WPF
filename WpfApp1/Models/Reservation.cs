using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class Reservation
    {
        public string UserName { get; }
        public RoomID RoomID { get; }
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }
        public TimeSpan Lenght => EndTime.Subtract(StartTime);
        public Reservation(RoomID roomID, DateTime startTime, DateTime endTime, string userName)
        {
            RoomID = roomID;
            StartTime = startTime;
            EndTime = endTime;
            UserName = userName;
        }
   
    }
}
