using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightInfoDisplay
{
    internal class Flight
    {
        private string flightNumber;
        private string origin;
        private string destination;
        private DateTime expectedDateTime;
        private string status;

        public string FlightNumber { get => flightNumber; set => flightNumber = value; }
        public string Origin { get => origin; set => origin = value; }
        public string Destination { get => destination; set => destination = value; }
        public DateTime ExpectedDateTime { get => expectedDateTime; set => expectedDateTime = value; }
        public string Status { get => status; set => status = value; }

        public Flight()
        {
        }

        public Flight(string flightNumber, string origin, string destination, DateTime expectedDateTime, string status)
        {
            FlightNumber = flightNumber;
            Origin = origin;
            Destination = destination;
            ExpectedDateTime = expectedDateTime;
            Status = status;
        }

        public double CalculateFees()
        {
            return 0;
        }

       
        public override string? ToString()
        {
            return base.ToString();
        }
    }
}
