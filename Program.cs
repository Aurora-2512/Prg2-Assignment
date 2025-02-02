﻿using System.Runtime.Intrinsics.X86;
using FlightInfoDisplay;
Dictionary<string, Airline> airlines= new Dictionary<string, Airline>();
Dictionary<string, BoardingGate> boardingGates = new Dictionary<string, BoardingGate>();
Dictionary<string, Flight> flight = new Dictionary<string, Flight>();

StreamReader sr = new StreamReader("airlines.csv");
sr.ReadLine();
while(!(sr.EndOfStream))
{
    string[] arr = sr.ReadLine().Split(',');
    airlines.Add(arr[1],new Airline(arr[0], arr[1]));
}
sr.Close();

StreamReader bg = new StreamReader("boardinggates.csv");
bg.ReadLine();
while(!(bg.EndOfStream))
{
    string[] arr = bg.ReadLine().Split(',');
    boardingGates.Add(arr[0], new BoardingGate(arr[0],bool.Parse(arr[1]),bool.Parse(arr[2]),bool.Parse(arr[3])));
}
sr.Close();


StreamReader fl = new StreamReader("flights.csv");
fl.ReadLine();
while(!(fl.EndOfStream))
{
    string[] arr = fl.ReadLine().Split(',');
    Flight f;
    if (arr[4].Equals("DDJB"))
    {
        f = new DDJBFlight(arr[0], arr[1], arr[2], DateTime.Parse(arr[3]));
    }
    else if (arr[4].Equals("CFFT"))
    {
        f = new CFFTFlight(arr[0], arr[1], arr[2], DateTime.Parse(arr[3]));
    }
    else if (arr[4].Equals("LWTT"))
    {
        f = new LWTTFlight(arr[0], arr[1], arr[2], DateTime.Parse(arr[3]));
    }
    else
    {
        f = new NORMFlight(arr[0], arr[1], arr[2], DateTime.Parse(arr[3]));
    }
    flight.Add(f.FlightNumber,f);
    string airlineCode = arr[0].Substring(0, 2);
    Airline airline = airlines[airlineCode];
    airline.AddFlight(f);
}

//displayFlightsInfo(flight);
//displayBoardingGate(boardingGate);
assignboardingGateToflight();
//createNewFlight();
displayFlightsOfAirline();

void displayFlightsInfo(Dictionary<string,Flight> flights)
{
    Console.WriteLine($"{"Flight Number",-14} {"Airline Name",-20} {"Origin",-22} {"Destination",-18} {"Expected Departure/Arrival Time",-40}");
    foreach(var flight in flights.Values){
        Console.WriteLine($"{flight.FlightNumber,-14} {airlines[flight.FlightNumber.Substring(0,2)].Name,-20} {flight.Origin,-22} {flight.Destination,-18} {flight.ExpectedDateTime,-40}");
    }
}

void displayBoardingGate(Dictionary<string,BoardingGate> boardingGates)
{
    Console.WriteLine($"{"Gate Name",-10} {"DDJB",-8} {"CFFT",-8} {"LWTT",-8}");
    foreach (var boardingGate in boardingGates.Values)
    {
        Console.WriteLine(boardingGate.ToString());
    }
}

//5
void assignboardingGateToflight()
{
    Console.Write("Enter Flight Number: ");
    string flightNo = Console.ReadLine();
    
    Flight fl = flight[flightNo];
    if (fl is NORMFlight)
    {
        fl=(NORMFlight)fl;
    }
    else if (fl is DDJBFlight)
    {
        fl = (DDJBFlight)fl;
    }
    else if (fl is LWTTFlight)
    {
        fl = (LWTTFlight)fl;
    }
    else 
    { 
        fl= (CFFTFlight)fl;
    }
    Console.WriteLine(fl.ToString());

    BoardingGate Gate;
    string boardingGateName;
    while (true)
    {
        Console.Write("Enter Boarding Gate Name: ");
        boardingGateName=Console.ReadLine();
        if (boardingGates[boardingGateName].F == null)
        {
            Gate = boardingGates[boardingGateName];
            Console.WriteLine($"Boarding Gate Name: {Gate.GateName}\nSupport DDJB: {Gate.SupportDDJB}\nSupport CFFT: {Gate.SupportCFFT}\nSupport LWTT: {Gate.SupportLWTT}");
            if(checkSRC(Gate,fl) is true)
            {
                Console.WriteLine();
                break; 
            }
            else
            {
                Console.WriteLine("Please choose the boarding gate again that support special request code");
            }
        }
        //Validation for not free 
    }
    
    Console.WriteLine("Would you like to update the status of the flight? (Y / N)");
    string opt = Console.ReadLine();
    string flightstatus="";
    if (opt.ToUpper() == "Y")
    {
        Console.Write("1. Delayed\n2. Boarding\n3. On Time\nPlease select the new status of the flight: ");
        int statusNo = int.Parse(Console.ReadLine());
        switch (statusNo)
        {
            case 1: flightstatus = "Delayed"; break;
            case 2: flightstatus = "Boarding"; break;
            case 3: flightstatus = "On Time"; break;
        }
    }
    else {
        flightstatus = "On Time";
    }
    fl.Status= flightstatus;
    Gate.F = fl;
    Console.WriteLine($"Flight {flightNo} has been assigned to boarding gate {boardingGateName}");
}

//check whether boarding gate support Special Request
bool checkSRC(BoardingGate bg,Flight f)
{
    if (f is NORMFlight)
    {
        return true;
    }
    else if (f is DDJBFlight)
    {
        if(bg.SupportDDJB)
            return true;
    }
    else if (f is LWTTFlight)
    {
        if(bg.SupportLWTT)
            return true;
    }
    else if(f is CFFTFlight)
    {
        if(bg.SupportCFFT)
            return true;
    }
    return false;
}

//6
void createNewFlight() 
{
    string choiceYesNo = "N";
    do{
        Console.Write("Enter Flight Number,Origin,Destination,Expected Arrival/Departure Time:  ");
        string[] arr = Console.ReadLine().Split(',');
        string flightNumber = arr[0];
        string origin = arr[1];
        string dest = arr[2];
        DateTime time = DateTime.Parse(arr[3]);
        Console.Write("Enter Y(yes) or N(no) for special request code: ");
        string opt = Console.ReadLine();
        string spCode = "";
        Flight newFlight = null;
        StreamWriter sw = new StreamWriter("flight.csv", append: true);
        if (opt.ToUpper() == "Y")
        {
            Console.Write("Choose 1,2, or 3 for special request code\n1. DDJB\n2. LWTT\n3. CFFT\nEnter Your choice: ");
            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    {
                        spCode = "DDJB";
                        newFlight = new DDJBFlight(flightNumber, origin, dest, time);
                        sw.WriteLine($"{newFlight.FlightNumber},{newFlight.Origin},{newFlight.Destination},{newFlight.ExpectedDateTime},{"DDJB"}");
                        break;
                    }
                case 2:
                    {
                        spCode = "LWTT";
                        newFlight = new LWTTFlight(flightNumber, origin, dest, time);
                        sw.WriteLine($"{newFlight.FlightNumber},{newFlight.Origin},{newFlight.Destination},{newFlight.ExpectedDateTime},{"LWTT"}");
                        break;
                    }
                case 3:
                    {
                        spCode = "CFFT";
                        newFlight = new CFFTFlight(flightNumber, origin, dest, time);
                        sw.WriteLine($"{newFlight.FlightNumber},{newFlight.Origin},{newFlight.Destination},{newFlight.ExpectedDateTime},{"CFFT"}");
                        break;
                    }
            }
        }
        else
        {
            newFlight = new NORMFlight(flightNumber, origin, dest, time);
        }
        sw.Close();
        flight.Add(newFlight.FlightNumber, newFlight);
        Console.WriteLine("Successfully added new Flight");
        Console.WriteLine("Do you want to add new flight, type Y(yes) or N(no)?");
        choiceYesNo = Console.ReadLine();
    }
    while (choiceYesNo.ToUpper() == "Y");
 
}

//7
void displayFlightsOfAirline()
{
    Console.WriteLine($"{"Airline Code",-15} {"Airline Name",-30}");
    foreach(var airline in airlines.Values)
    {
        Console.WriteLine(airline.ToString());
    }
    Console.Write("Enter an airline code to check available flight on this airline: ");
    string aircode=Console.ReadLine();
    Airline selectedAirline= airlines[aircode];
    Console.WriteLine($"Flights of {selectedAirline.Name}:");
    Dictionary<string,Flight> flightfromAirLine=selectedAirline.Flights;
    Console.WriteLine($"{"Flight Number",-15} {"Origin",-20} {"Destination",-20}");
    foreach(var flight in flightfromAirLine.Values) 
    { 
        Console.WriteLine($"{flight.FlightNumber,-15} {flight.Origin,-20} {flight.Destination,-20}"); 
    }
    Console.Write("Enter Flight Number to details: ");
    string fNumber=Console.ReadLine();
    
   // Console.WriteLine($"{"Flight Number",-14} {"Airline Name",-20} {"Origin",-22} {"Destination",-18} {"Expected Departure/Arrival Time",-40} {"Special Request Code",-25} {"Boarding Gate",-15}");
    Flight fl = flightfromAirLine[fNumber];
    if (fl is NORMFlight)
    {
        fl = (NORMFlight)fl;
    }
    else if (fl is DDJBFlight)
    {
        fl = (DDJBFlight)fl;
    }
    else if (fl is LWTTFlight)
    {
        fl = (LWTTFlight)fl;
    }
    else
    {
        fl = (CFFTFlight)fl;
    }
    Console.WriteLine(fl.ToString());
    foreach(var bg in boardingGates.Values)
    { 
        if(bg.F != null)
        {
            if (bg.F.FlightNumber == fl.FlightNumber)
            {
                Console.WriteLine($"Boarding Gate: {bg.GateName}");
            }
        }
        
    }

}

