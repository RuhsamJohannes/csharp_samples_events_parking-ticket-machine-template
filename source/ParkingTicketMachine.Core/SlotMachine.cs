﻿using System;

namespace ParkingTicketMachine.Core
{
    public class SlotMachine
    {

        private int _totalAmount = 0;
        private DateTime _endOfParking;
        private string _location;

        public Ticket CurrentTicket { get; private set; }

        public SlotMachine(string location)
        {
            _location = location;
        }

        public DateTime EndOfParking { get => _endOfParking; }

        public int InsertCoin(int coin)
        {
            if (coin == 1 || coin == 2)
            {
                coin = coin * 100;
            }
            this._totalAmount += coin;

            return _totalAmount;
        }

        public DateTime TimeEndOfParking()
        {
            FastClock clock = FastClock.Instance;
            _endOfParking = clock.Time;

            DateTime t0 = clock.Time.Date;
            DateTime t1 = clock.Time.Date.AddHours(18);
            DateTime t2 = clock.Time.Date.AddHours(8);


            //wenn vor hinzufügen der Zeit die Uhrzeit zwischen 18:00 und 08:00 ist wird die Zeit auf 08:00 nächsten Tag gestellt.
            if (t0 < _endOfParking && _endOfParking < t2 || t1 < _endOfParking && _endOfParking < t0.AddDays(1))
            {
                _endOfParking = t2;
            }

            if (_totalAmount >= 50 && _totalAmount < 100)
            {
                _endOfParking = _endOfParking.AddMinutes(30);
            }
            else if (_totalAmount >= 100 && _totalAmount < 150)
            {
                _endOfParking = _endOfParking.AddMinutes(60);
            }
            else if (_totalAmount >= 150)
            {
                _endOfParking = _endOfParking.AddMinutes(90);
            }

            //wenn nach hinzufügen der zeit die Uhrzeit zwischen 18:00 und 08:00 ist werden 14 stunden hinzugefügt.
            if (t0 < _endOfParking && _endOfParking < t2 || t1 < _endOfParking && _endOfParking < t0.AddDays(1))
            {
                _endOfParking = _endOfParking.AddHours(14);
            }

            return _endOfParking;
        }

        public void PrintTicket(EventHandler<Ticket> ticketReady)
        {
            CurrentTicket = new Ticket(_totalAmount, _location, _endOfParking);
            CurrentTicket.PrintedTicket += ticketReady;
            CurrentTicket.PrintTicket();

        }

        public void DeleteAllInfo()
        {
            _endOfParking = DateTime.Now;
            _totalAmount = 0;
        }
    }
}
