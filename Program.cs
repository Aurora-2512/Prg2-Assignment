using System.Runtime.Intrinsics.X86;
using FlightInfoDisplay;
Dictionary<string, Airline> airlines= new Dictionary<string, Airline>();
Dictionary<string, BoardingGate> boardingGates = new Dictionary<string, BoardingGate>();
Dictionary<string, Flight> flight = new Dictionary<string, Flight>();

StreamReader sr = new StreamReader("airlines.csv");
sr.ReadLine();
while(!(sr.EndOfStream))
{
    string[] arr = sr.ReadLine().Split(',');
    airlines.Add(arr[1],new Airline(arr[0], arr[1],new Dictionary<string, Flight>()));
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
        f = new DDJBFlight(arr[0], arr[1], arr[2], DateTime.Parse(arr[3]), "", 0);
    }
    else if (arr[4].Equals("CFFT"))
    {
        f = new CFFTFlight(arr[0], arr[1], arr[2], DateTime.Parse(arr[3]), "", 0);
    }
    else if (arr[4].Equals("LWTT"))
    {
        f = new LWTTFlight(arr[0], arr[1], arr[2], DateTime.Parse(arr[3]), "", 0);
    }
    else
    {
        f = new NORMFlight(arr[0], arr[1], arr[2], DateTime.Parse(arr[3]), "");
    }
    flight.Add(f.FlightNumber,f);
}

//displayFlightsInfo(flight);
//displayBoardingGate(boardingGate);
assignboardingGateToflight();
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


