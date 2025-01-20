using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightInfoDisplay
{
    internal class BoardingGate
    {
        private string gateName;
        private bool supportCFFT;
        private bool supportDDJB;
        private bool supportLWTT;
        private Flight flight;

        public BoardingGate(string gateName, bool supportDDJB, bool supportCFFT, bool supportLWTT, Flight flight)
        {
            this.GateName = gateName;
            this.supportCFFT = supportCFFT;
            this.supportDDJB = supportDDJB;
            this.supportLWTT = supportLWTT;
            this.flight = flight;
        }

        public string GateName { get => gateName; set => gateName = value; }

        public double calculateFees()
        {
            return 0;
        }

        public override string ToString()
        {
            return $"{GateName,-10} {supportDDJB,-8} {supportCFFT,-8} {supportLWTT,-8}";
        }
    }

}
