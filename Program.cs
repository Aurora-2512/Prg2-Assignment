using System.Runtime.Intrinsics.X86;
using FlightInfoDisplay;
Dictionary<string, Airline> airlines= new Dictionary<string, Airline>();
Dictionary<string, BoardingGate> boardingGate = new Dictionary<string, BoardingGate>();
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
    boardingGate.Add(arr[0], new BoardingGate(arr[0],bool.Parse(arr[1]),bool.Parse(arr[2]),bool.Parse(arr[3]),new Flight()));
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
displayBoardingGate(boardingGate);

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

