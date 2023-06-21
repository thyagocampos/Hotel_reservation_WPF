using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace WpfApp1.DTO
{
    public class ReservationDTO
    {
        [Key]
        public Guid Id { get; set; }

        public int FloorNumber { get; set; }
        public int RoomNumber { get; set; }
        public string UserName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
