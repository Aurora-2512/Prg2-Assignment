﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightInfoDisplay
{
    internal class LWTTFlight:Flight
    {
        private double requestFee;

        public double RequestFee { get => requestFee; set => requestFee = value; }


        public LWTTFlight(string flightNumber, string origin, string destination, DateTime expectedDateTime) : base(flightNumber, origin, destination, expectedDateTime)
        {
            
        }

        public double CalculateFees()
        {
            throw new NotImplementedException();
        }

        public override string? ToString()
        {
            return base.ToString() + $"Special Request Code: LWTT";
        }
    }
}
