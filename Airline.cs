using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightInfoDisplay
{
    internal class Airline
    {
        private string name;
        private string code;
        private Dictionary<string, Flight> flights;

        public string Name { get => name; set => name = value; }
        public string Code { get => code; set => code = value; }
        internal Dictionary<string, Flight> Flights { get => flights; set => flights = value; }

        public Airline(string name, string code, Dictionary<string, Flight> flights)
        {
            this.Name = name;
            this.Code = code;
            this.Flights = flights;
        }

        public bool AddFlight(Flight f)
        {
            Flights.Add(f.FlightNumber, f);
            return true;
        }

        public bool RemoveFlight(Flight f)
        {
            {
                Flights.Remove(f.FlightNumber);
                return true;
            }
        }

        public double calculateFees()
        {
            double fees = 0;
            return fees;
        }

        public override string? ToString()
        {
            return base.ToString();
        }
    }
}
