namespace Transport_System_Process;
using System.Text.RegularExpressions;

//Powolanie parametrow dla klasy Bus - Punkt w Scorecard: Atrybuty własne
[Parameters(0.25, 0.9, 0.1, 1.2, 1.7, 1.5, 1.2, 7, 00)]

//Wykorzystanie Interface - Punkt w Scorecard: Kontrakty: Interface
public class Tram : MeansOfTransport
{
    //Pobranie atrybutow dla klasy Bus - Punkt w Scorecard: Atrybuty własne
    Parameters parameters = (Parameters)Attribute.GetCustomAttribute(typeof(Tram), typeof(Parameters));
    private string meansOfTransport = "Tram";
    public Dictionary<string, Dictionary<string, string>> Timetable = new Dictionary<string, Dictionary<string, string>>();
    private static object _Monitor = new object();
    static string directory = Directory.GetCurrentDirectory().Substring(0, Directory.GetCurrentDirectory().Length - 41);
    private string UserMeansOfTransport;
    private string UserLineNum;
    private string UserStop;
    private string UserDirection;
    private int UserHour;
    private int UserMinutes;
    public bool End = false;
    public int iterator;
    
    private Random _random = new Random();

    //Asynchroniczne wywolanie funkcji FileReader() - Punkt w Scorecard: Metody async
    public async Task FileReader()
    {
        //Asynchroniczne wywolanie funkcji FileReader() - Punkt w Scorecard: Metody async
        await Task.Run(() =>
        {
            string FileName = "Tram.txt";
            string path = $"{directory}{FileName}";
            bool turn = false;
            string Key = "";
            string Stop = "";
            string TimeBetweenStops = "";
            
            //Wykozystanie zaprojektowanego typu generycznego - Punkt w Scorecard: Zaprojektowanie typow generycznych
            //Wykożystanie typow wyliczeniowych - Punkt w Scorecard: Typy wyliczeniowe
            //Wykożystanie LINQ - Punkt w Scorecard: Kolekcje danych i Language Integrated Query
            //Wykorzystanie wyrazenia lambda - Punkt w Scorecard: Wyrazenia Lambda
            var filelist = new MyGenericList<string>();
            IEnumerable<string> Lines = filelist.getFileList(path).Where(L => L[0] == 'T');
            IEnumerable<string> Stops = filelist.getFileList(path).Where(S => S[0] != 'T');

            Dictionary<string, string> GivenTimetable = new Dictionary<string, string>();
            
            //Wykorzystanie monitora do synchronizacji dizalania obu taskow - Punkt w Scorecard: Wykorzystanie synchronization primitives
            Task task_1 = Task.Run(() =>
            {
                foreach (var line in Lines)
                {
                    lock (_Monitor)
                    {
                        while (turn)
                        {
                            Monitor.Wait(_Monitor);
                        }

                        Key = line;
                        turn = true;
                        Monitor.Pulse(_Monitor);
                    }
                }
            });

            Task task_2 = Task.Run(() =>
            {
                foreach (var stop in Stops)
                {
                    lock (_Monitor)
                    {
                        while (!turn)
                        {
                            Monitor.Wait(_Monitor);
                            GivenTimetable.Clear();
                        }

                        if (stop.StartsWith("="))
                        {
                            Timetable.Add(Key, GivenTimetable.ToDictionary(entry => entry.Key, entry => entry.Value));
                            turn = false;
                            Monitor.Pulse(_Monitor);
                        }
                        else
                        {
                            //Wykozystanie wyrazen regularnych - Punkt w Scorecard: Wykozystanie wyrazen regularnych
                            MatchCollection matches = Regex.Matches(stop, parameters.pattern);
                            Stop = matches[0].ToString();
                            TimeBetweenStops = matches[1].ToString();
                            GivenTimetable.Add(Stop, TimeBetweenStops);
                        }
                    }
                }
            });
            Task.WaitAll(task_1, task_2);
        });
        Console.WriteLine("Tram End");
    }

    public void CalculationOfDelay()
    {
        //Zdefiniowanie funkcji anonimowej - Punkt w Scorecard: Metody anonimowe
        Func<double, bool> check = (x) => x >= ((parameters.StartOfPeakHours_H * 60) + parameters.StartOfPeakHours_Min) 
                                          && x <= ((parameters.EndOfPeakHours_H * 60) + parameters.EndOfPeakHours_Min);
        int Time = (UserHour - parameters.StartHour_H) * 60 + UserMinutes;//czas przybycia pasazera na przystanek
        int StopsIter = 0; //aktalny przystanek 
        double CurrentTime = 0; //aktualny czas autobusu
        double Delay = 0;
        double WaitingTime = 0; //pasazera na autobus 
        string CurrentBusDirection = "0";
        int CurrentStop = 0;
        string BusDirection;
        bool toEarly = CurrentTime > Time ? true : false;
        
        if(!toEarly)
        {
            //obliczanie aktualnego stanu pojazdu
            while (CurrentTime < Time) //potrzebny aby autobus jedzil w kolko 
            {
                while (CurrentTime < Time && StopsIter < Timetable[UserLineNum].Count-1) //droga w od 1 do ostantiego przystanku
                {
                    StopsIter++;
                    CurrentBusDirection = Timetable[UserLineNum].ElementAt(Timetable[UserLineNum].Count - 1).Key;
                    if (_random.NextDouble() >= parameters.ChanceOfDelay)
                    {
                        Delay = 1;
                    }
                    else if (_random.NextDouble() <= parameters.ChanceOfSmallDelay)
                    {
                        Delay = parameters.SmallDelay * (CurrentTime >= ((parameters.StartOfPeakHours_H * 60) + parameters.StartOfPeakHours_Min) 
                                                         && CurrentTime <= ((parameters.EndOfPeakHours_H * 60) + parameters.EndOfPeakHours_Min) ? 
                            parameters.DelayDuringPeakHours: parameters.DelayDuringNormalHours);
                    }
                    else
                    {
                        Delay = parameters.BigDelay * (CurrentTime >= ((parameters.StartOfPeakHours_H * 60) + parameters.StartOfPeakHours_Min) 
                                                       && CurrentTime <= ((parameters.EndOfPeakHours_H * 60) + parameters.EndOfPeakHours_Min) ? 
                            parameters.DelayDuringPeakHours: parameters.DelayDuringNormalHours);
                    }
                    CurrentTime += int.Parse(Timetable[UserLineNum].ElementAt(StopsIter).Value) * Delay;
                }
            
                while (CurrentTime < Time && StopsIter > 0) //droga od ostatniego do pierwszego przystanku
                {
                    StopsIter--;
                    CurrentBusDirection = Timetable[UserLineNum].ElementAt(0).Key;
                
                    if (_random.NextDouble() >= parameters.ChanceOfDelay)
                    {
                        Delay = 1;
                    }
                    else if (_random.NextDouble() <= parameters.ChanceOfSmallDelay)
                    {
                        //Wywolanie funkcji anonimowej - Punkt w Scorecard: Metody anonimowe
                        Delay = parameters.SmallDelay * (check(CurrentTime) ? parameters.DelayDuringPeakHours : parameters.DelayDuringNormalHours);
                    }
                    else
                    {
                        //Wywolanie funkcji anonimowej - Punkt w Scorecard: Metody anonimowe
                        Delay = parameters.BigDelay * (check(CurrentTime) ? parameters.DelayDuringPeakHours : parameters.DelayDuringNormalHours);
                    }
                    CurrentTime += int.Parse(Timetable[UserLineNum].ElementAt(StopsIter+1).Value) * Delay;
                }
            }

            CurrentStop = StopsIter;
            BusDirection = CurrentBusDirection;
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Calculating Delay...");
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------");

            //obliczanie opoznienia
            while (CurrentBusDirection != UserDirection || Timetable[UserLineNum].ElementAt(StopsIter).Key != UserStop)
            {
                if (CurrentBusDirection != Timetable[UserLineNum].ElementAt(0).Key && StopsIter < Timetable[UserLineNum].Count-1) //pojazd jedzie z 1 do ostatiego przystanku
                {
                    StopsIter++;
                    WaitingTime += int.Parse(Timetable[UserLineNum].ElementAt(StopsIter).Value);
                }
                else if (CurrentBusDirection != Timetable[UserLineNum].ElementAt(0).Key && StopsIter == Timetable[UserLineNum].Count - 1)//zawracanie pojazdu
                {
                    CurrentBusDirection = Timetable[UserLineNum].ElementAt(0).Key;
                }
                else if (CurrentBusDirection == Timetable[UserLineNum].ElementAt(0).Key && StopsIter > 0)//pojazd jedzie z ostatnego do pierwego przystanku
                {
                    StopsIter--;
                    WaitingTime += int.Parse(Timetable[UserLineNum].ElementAt(StopsIter+1).Value);
                }
                else if (CurrentBusDirection == Timetable[UserLineNum].ElementAt(0).Key && StopsIter == 0)//zawracanie pojazdu
                {
                    CurrentBusDirection = Timetable[UserLineNum].ElementAt(Timetable[UserLineNum].Count - 1).Key;
                }
            }
            Console.WriteLine($"Line: {UserLineNum}");
            Console.WriteLine($"Destination Stop: {Timetable[UserLineNum].ElementAt(StopsIter).Key}");
            Console.WriteLine($"Waiting Time: {WaitingTime}");
            Console.WriteLine($"Current Stop of Transport: {Timetable[UserLineNum].ElementAt(CurrentStop).Key}");
            Console.WriteLine($"Current Direction of Transport: {BusDirection}");
            
            //Wywolanie metody opdowiadajacej za komunikacje miedzyprocesowa - Punkt w Scorecard: Dwustronna komunikacja miedzyprocesowa
            Sender(Timetable[UserLineNum].ElementAt(CurrentStop).Key, WaitingTime, toEarly);
        }
        else
        {
            //Wywolanie metody opdowiadajacej za komunikacje miedzyprocesowa - Punkt w Scorecard: Dwustronna komunikacja miedzyprocesowa
            Sender(Timetable[UserLineNum].ElementAt(CurrentStop).Key, WaitingTime, toEarly);
        }
    }

    //Utworzenie metody opdowiadajacej za komunikacje miedzyprocesowa (wysyla dane do drugiego procesu) - Punkt w Scorecard: Dwustronna komunikacja miedzyprocesowa
    //Asynchroniczne wywolanie funkcji Getter() - Punkt w Scorecard: Metody async
    public async Task Getter()
    {
        string UserData = $"{directory}UserData{iterator}.txt";
        string EndData = $"{directory}EndDataT.txt";
        
        //Asynchroniczne wywolanie funkcji Getter() - Punkt w Scorecard: Metody async
        await Task.Run(() =>
        {
            while (!File.Exists(UserData) && !File.Exists(EndData))
            {
                Thread.Sleep(1000);
            }

            if (File.Exists(UserData))
            {
                using (var reader = new StreamReader(UserData))
                {
                    UserMeansOfTransport = reader.ReadLine();
                    UserLineNum = reader.ReadLine();
                    UserStop = reader.ReadLine();
                    UserDirection = reader.ReadLine();
                    UserHour = int.Parse(reader.ReadLine());
                    UserMinutes = int.Parse(reader.ReadLine());
                }
                if (UserLineNum.First() == 'T')
                {
                    End = false;
                    CalculationOfDelay();
                }
            }
            else if (File.Exists(EndData))
            {
                End = true;
                
                //Wywolanie funkcji Dispose() - Punkt w Scorecard: Implementacja inteface IDisposeable
                Dispose();
            }
        });
    }

    //Utworzenie metody opdowiadajacej za komunikacje miedzyprocesowa (odbiera dane od drugiego procesu) - Punkt w Scorecard: Dwustronna komunikacja miedzyprocesowa
    public void Sender(string CurrentStop, double WaitingTime, bool toEarly)
    {
        string SystemData = $"{directory}SystemData.txt";
        using (var writer = new StreamWriter(SystemData))
        {
            if(!toEarly)
            {
                writer.WriteLine(CurrentStop);
                writer.WriteLine(WaitingTime);
                writer.Flush();
            }
            else
            {
                writer.WriteLine("Error");
                writer.WriteLine($"It's to early for this line, first tram will come at {parameters.StartHour_H}:{parameters.StartHour_Min}0");
                writer.Flush();
            }
        }
    }
    
    //Zdefiniowanie funkcji Dispose - Punkt w Scorecard: Implementacja interface IDisposeable
    public void Dispose()
    {
        string EndData = $"{directory}EndDataT.txt";
        File.Delete(EndData);
    }
    
    public int getIterator
    {
        get { return iterator; }
        set { iterator = value; }
    }
}