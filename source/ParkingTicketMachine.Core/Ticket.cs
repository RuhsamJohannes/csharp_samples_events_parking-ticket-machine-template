using System;

namespace ParkingTicketMachine.Core
{
    public class Ticket
    {
        private int totalAmount;

        public DateTime TimePrinted { get; set; }
        public DateTime EndOfParking { get; set; }
        public string Location { get; set; }
        public int PaidPrice { get; set; }

        public event EventHandler<Ticket> PrintedTicket;


        public Ticket(int totalAmount, string location, DateTime endOfParking)
        {
            PaidPrice = totalAmount;
            Location = location;
            TimePrinted = FastClock.Instance.Time;
            EndOfParking = endOfParking;
        }

        internal void PrintTicket()
        {
            PrintedTicket?.Invoke(this, this);
        }
    }
}
