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

        public Airline(string name, string code)
        {
            this.Name = name;
            this.Code = code;
            flights = new Dictionary<string, Flight>();
        }

        public void AddFlight(Flight f)
        {
            Flights.Add(f.FlightNumber, f);
            
        }

        public void RemoveFlight(Flight f)
        {
            {
                Flights.Remove(f.FlightNumber);
            }
        }

        public double calculateFees()
        {
            double fees = 0;
            return fees;
        }

        public override string? ToString()
        {
            return $"{Code,-15} {Name,-30}";
        }
    }
}
