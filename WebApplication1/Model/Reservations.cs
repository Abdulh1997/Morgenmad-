using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Model
{
    public class Reservations
    {
        public long ReservationsID { get; set; }

        public DateTime Date { get; set; }

        public int NumberOfAdults { get; set; }

        public int NumberOfChildren { get; set; }

        public int RoomNumber{ get; set; }


    }
}
