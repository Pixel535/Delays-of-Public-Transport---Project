namespace Waiting_Passenger_Process;
using System.IO;

//Polimorfizm, Dziedziczenie klas, stworzenie klasy maciezystej - Punkt w Scorecard: Polimorfizm
public class MeansOfTransport
{
    private string meansOfTransport = "vehicle";
    List<string> LineNum = new List<string>();
    List<string> DirectionsAndStops = new List<string>();
    public string PickedLine = "";
    public static string directory = Directory.GetCurrentDirectory().Substring(0, Directory.GetCurrentDirectory().Length - 42);
    
    //Utworzenie wirtualnych funkcji w klasie macieżystej - Punkt w Scorecard: Polimorfizm
    public virtual String getMeansOfTransport() { return meansOfTransport; }
    
    //Wykożystanie typow wyliczeniowych - Punkt w Scorecard: Typy wyliczeniowe
    public virtual IEnumerable<string> getLineNum() { return LineNum; }
    public virtual List<string> getDirectionAndStops() { return DirectionsAndStops; }

    public void LineNumPrinter(string file)
    {
        Console.WriteLine($"These are available Line numbers of {getMeansOfTransport()}");
        string path = $"{directory}{file}";
        
        //Wykożystanie typow wyliczeniowych - Punkt w Scorecard: Typy wyliczeniowe
        foreach (var LineNumber in getLineNum())
        {
            Console.Write($"{LineNumber} ");
        }
    }
    
    public void DirectionPrinter(string PickedLine)
    {
        var DirectionList = getDirectionAndStops();
        Console.WriteLine($"These are available Directions of {PickedLine} Line:");
        
        //Wykożystanie LINQ - Punkt w Scorecard: Kolekcje danych i Language Integrated Query
        Console.WriteLine($"* End Stop #1: {DirectionList.First()}");
        Console.WriteLine($"* End Stop #2: {DirectionList.Last()}");
    }
    
    public void StopsPrinter(string PickedLine)
    {
        var StopsList = getDirectionAndStops();
        Console.WriteLine($"These are available Stops of {PickedLine} Line");
        foreach (var stops in StopsList)
        {
            Console.Write($"{stops} ");
        }
    }
}