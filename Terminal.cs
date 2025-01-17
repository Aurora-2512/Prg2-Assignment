using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightInfoDisplay
{
    internal class Terminal
    {
        private string terminalName;
        private Dictionary<string, Airline> airlines;
        private Dictionary<string, Flight> flights;
        private Dictionary<string,BoardingGate> boardingGates;
        private Dictionary<string,double> gates;

        public Terminal(string terminalName, Dictionary<string, Airline> airlines, Dictionary<string, Flight> flights, Dictionary<string, BoardingGate> boardingGates, Dictionary<string, double> gates)
        {
            this.terminalName = terminalName;
            this.airlines = airlines;
            this.flights = flights;
            this.boardingGates = boardingGates;
            this.gates = gates;
        }

        public bool AddAirline(Airline airline)
        {
            airlines.Add(airline.Code, airline);
            return true;
        }

        public bool AddBoardingGate(BoardingGate boardingGate) 
        {
            boardingGates.Add(boardingGate.GateName, boardingGate);
            return true;
        }

        public Airline GetAirlineFromFlight(Flight f)
        {
            return null;
        }

        public void printAirlineFees()
        {

        }

        public override string? ToString()
        {
            return base.ToString();
        }
    }
}
