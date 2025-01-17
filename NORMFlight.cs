using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightInfoDisplay
{
    internal class NORMFlight : Flight
    {
        public NORMFlight(string flightNumber, string origin, string destination, DateTime expectedDateTime, string status) : base(flightNumber, origin, destination, expectedDateTime, status)
        {
        }

        public override double CalculateFees()
        {
            throw new NotImplementedException();
        }

        public override string ToString() 
        {
            return base.ToString();
        }
    }
}
