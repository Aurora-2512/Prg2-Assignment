using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightInfoDisplay
{
    internal class NORMFlight : Flight
    {
        public NORMFlight(string flightNumber, string origin, string destination, DateTime expectedDateTime) : base(flightNumber, origin, destination, expectedDateTime)
        {
        }

        public double CalculateFees()
        {
            throw new NotImplementedException();
        }

        public override string ToString() 
        {
            return base.ToString()+$"Special Request Code: None";
        }
    }
}
