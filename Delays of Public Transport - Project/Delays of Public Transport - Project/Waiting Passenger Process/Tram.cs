namespace Waiting_Passenger_Process;
using System.IO;
using System.Text.RegularExpressions;

//Powolanie parametrow dla klasy Tram - Punkt w Scorecard: Atrybuty własne
[Parameters("Tram.txt", "Tram", 'T')]

//Polimorfizm, Dziedziczenie klas - Punkt w Scorecard: Polimorfizm
public class Tram : MeansOfTransport
{
    //Pobranie atrybutow dla klasy Tram - Punkt w Scorecard: Atrybuty własne
    static Parameters parameters = (Parameters)Attribute.GetCustomAttribute(typeof(Tram), typeof(Parameters));
    
    private static string path = $"{directory}{parameters.FileName}";
    List<string> LineNum = new List<string>();
    
    //Override funkcji z klasy macieżystej - Punkt w Scorecard: Polimorfizm
    public override String getMeansOfTransport() { return parameters.meansOfTransport; }

    //Override funkcji z klasy macieżystej - Punkt w Scorecard: Polimorfizm
    //Wykożystanie typow wyliczeniowych - Punkt w Scorecard: Typy wyliczeniowe
    //Wykożystanie LINQ - Punkt w Scorecard: Kolekcje danych i Language Integrated Query
    //Wykozystanie zaprojektowanego typu generycznego - Punkt w Scorecard: Zaprojektowanie typow generycznych
    //Wykorzystanie wyrazenia lambda - Punkt w Scorecard: Wyrazenia Lambda
    public override IEnumerable<string> getLineNum()
    {
        var filelist = new MyGenericList<string>();
        IEnumerable<string> Lines = filelist.ReadFromFile(path).Where(L => L[0] == parameters.meansOfTransportLetter);
        return Lines;
    }
    
    //Override funkcji z klasy macieżystej - Punkt w Scorecard: Polimorfizm
    //Wykozystanie zaprojektowanego typu generycznego - Punkt w Scorecard: Zaprojektowanie typow generycznych
    //Wykożystanie typow wyliczeniowych - Punkt w Scorecard: Typy wyliczeniowe
    public override List<string> getDirectionAndStops()
    {
        List<string> DirectionsAndStops = new List<string>();
        var filelist = new MyGenericList<string>();
        IEnumerable<string> enumerableDirections = filelist.ReadFromFile(path);
        bool firstStop = false;

        foreach (var direction in enumerableDirections)
        {
            if (direction[0] == PickedLine[0] && direction[1] == PickedLine[1] && direction.Length == PickedLine.Length)
            {
                firstStop = true;
            }
            else if (firstStop)
            {
                if (direction[0] != '=')
                {
                    //Wykozystanie wyrazen regularnych - Punkt w Scorecard: Wykozystanie wyrazen regularnych
                    MatchCollection matches = Regex.Matches(direction, parameters.pattern);
                    DirectionsAndStops.Add(matches[0].ToString());
                }
                else
                {
                    firstStop = false;
                }
            }
        }
        return DirectionsAndStops;
    }
}