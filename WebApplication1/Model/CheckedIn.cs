namespace WebApplication1.Model
{
    public class CheckedIn
    {
        public long CheckedInId { get; set; }

        public DateTime Date { get; set; }

        public int NumberOfAdults { get; set; }

        public int NumberOfChildren { get; set; }

        public int RoomNumber { get; set; }
    }
}
