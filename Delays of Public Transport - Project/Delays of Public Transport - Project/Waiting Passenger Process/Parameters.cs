namespace Waiting_Passenger_Process;

//Utworzenie parametrow dla klas - Punkt w Scorecard: Atrybuty własne
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]

public class Parameters : Attribute
{
    public string FileName { get; set; }
    public string meansOfTransport { get; set; }
    public char meansOfTransportLetter { get; set; }
    
    //Zdefiniowanie wzoru do czytania pliku - Punkt w Scorecard: Wykorzystanie wyrazen regularnych
    public string pattern { get; set; } = @"[0-9]{1,2}";
    
    public Parameters(string FileName, string meansOfTransport, char meansOfTransportLetter)
    {
        this.FileName = FileName;
        this.meansOfTransport = meansOfTransport;
        this.meansOfTransportLetter = meansOfTransportLetter;
    }
}