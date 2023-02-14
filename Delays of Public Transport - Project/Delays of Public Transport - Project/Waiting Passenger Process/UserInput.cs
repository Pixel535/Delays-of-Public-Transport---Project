namespace Waiting_Passenger_Process;

//Zastosowanie interface IDisposeable - Punkt w Scorecard: Implementacja interface'u IDisposeable
public class UserInput : IDisposable
{
    private string UserMeansOfTransport;
    private string UserLineNum;
    private string UserDirection;
    private string UserStop;
    private bool Direction;
    private int Hour;
    private int Minutes;
    private Random _random = new Random();
    private int CurrentStop;
    private double WaitingTime;
    public int iterator = 0;
    string directory = Directory.GetCurrentDirectory().Substring(0, Directory.GetCurrentDirectory().Length - 42);

    public void GetData()
    {
        MeansOfTransport meansOfTransport = null;

        bool meansOfTransportLoop = true;
        while (meansOfTransportLoop)
        {
            TextPrinter();
            UserMeansOfTransport = Console.ReadLine().ToUpper();
            if (UserMeansOfTransport == "BUS")
            {
                Bus bus = new Bus();
                bus.LineNumPrinter($"{bus.getMeansOfTransport()}.txt");
                meansOfTransport = bus;
                meansOfTransportLoop = false;
            }
            else if (UserMeansOfTransport == "TRAM")
            {
                Tram tram = new Tram();
                tram.LineNumPrinter($"{tram.getMeansOfTransport()}.txt");
                meansOfTransport = tram;
                meansOfTransportLoop = false;
            }
            else if (UserMeansOfTransport == "TRAIN")
            {
                Train train = new Train();
                train.LineNumPrinter($"{train.getMeansOfTransport()}.txt");
                meansOfTransport = train;
                meansOfTransportLoop = false;
            }
            else if (UserMeansOfTransport == "METRO")
            {
                Metro metro = new Metro();
                metro.LineNumPrinter($"{metro.getMeansOfTransport()}.txt");
                meansOfTransport = metro;
                meansOfTransportLoop = false;
            }
            else
            {
                Console.WriteLine("That is not on the public transport list! Try again!");
                Console.WriteLine("*******************************************************************************************************\n");
                Console.ReadLine();
            }
        }
        
        bool LineNumLoop = true;
        while (LineNumLoop)
        {
            TextPrinter();
            Console.WriteLine($"{meansOfTransport.getMeansOfTransport()}");
            meansOfTransport.LineNumPrinter($"{meansOfTransport.getMeansOfTransport()}.txt");
            Console.WriteLine("\n\nChoose your Line Number:");
            UserLineNum = Console.ReadLine().ToUpper();
            
            //Wykożystanie typow wyliczeniowych - Punkt w Scorecard: Typy wyliczeniowe
            //Wykożystanie LINQ - Punkt w Scorecard: Kolekcje danych i Language Integrated Query
            IEnumerable<string> LineNumEnumerable = meansOfTransport.getLineNum();
            if(LineNumEnumerable.Contains(UserLineNum))
            {
                LineNumLoop = false;
            }
            else
            {
                Console.WriteLine("That is not on the Line numbers list! Try again!");
                Console.WriteLine("*******************************************************************************************************\n");
                Console.ReadLine();
            }
        }

        bool StopLoop = true;
        while (StopLoop)
        {
            TextPrinter();
            Console.WriteLine($"{meansOfTransport.getMeansOfTransport()}");
            meansOfTransport.LineNumPrinter($"{meansOfTransport.getMeansOfTransport()}.txt");
            Console.WriteLine("\n\nChoose your Line Number:");
            Console.WriteLine($"{UserLineNum}");
            meansOfTransport.PickedLine = UserLineNum;
            meansOfTransport.StopsPrinter(UserLineNum);
            Console.WriteLine("\n\nChoose your stop:");
            UserStop = Console.ReadLine().ToUpper();
            
            //Wykożystanie typow wyliczeniowych - Punkt w Scorecard: Typy wyliczeniowe
            //Wykożystanie LINQ - Punkt w Scorecard: Kolekcje danych i Language Integrated Query
            IEnumerable<string> StopsEnumerable = meansOfTransport.getDirectionAndStops();
            if(StopsEnumerable.Contains(UserStop))
            {
                StopLoop = false;
            }
            else
            {
                Console.WriteLine("That is not on the stops list of the picked Line! Try again!");
                Console.WriteLine("*******************************************************************************************************\n");
                Console.ReadLine();
            }
        }
        
        bool DirectionLoop = true;
        while (DirectionLoop)
        {
            TextPrinter();
            Console.WriteLine($"{meansOfTransport.getMeansOfTransport()}");
            meansOfTransport.LineNumPrinter($"{meansOfTransport.getMeansOfTransport()}.txt");
            Console.WriteLine("\n\nChoose your Line Number:");
            Console.WriteLine($"{UserLineNum}");
            meansOfTransport.PickedLine = UserLineNum;
            meansOfTransport.StopsPrinter(UserLineNum);
            Console.WriteLine("\n\nChoose your stop:");
            Console.WriteLine($"{UserStop}");
            meansOfTransport.DirectionPrinter(UserLineNum);
            UserDirection = Console.ReadLine().ToUpper();
            if(meansOfTransport.getDirectionAndStops().First() == UserDirection)
            {
                Direction = false;
                DirectionLoop = false;
            }
            else if (meansOfTransport.getDirectionAndStops().Last() == UserDirection)
            {
                Direction = true;
                DirectionLoop = false;
            }
            else
            {
                Console.WriteLine("That is not on the Directions list! Try again!");
                Console.WriteLine("*******************************************************************************************************\n");
                Console.ReadLine();
            }
        }

        Hour = _random.Next(0, 24);
        Minutes = _random.Next(0, 61);
        
        //Wywolanie metod opdowiadajacych za komunikacje miedzyprocesowa - Punkt w Scorecard: Dwustronna komunikacja miedzyprocesowa
        Sender(UserMeansOfTransport, UserLineNum, UserStop, UserDirection, Hour, Minutes);
        Getter();
    }

    static void TextPrinter()
    {
        Console.WriteLine("Choose your means of transport:");
        Console.WriteLine("- Bus");
        Console.WriteLine("- Tram");
        Console.WriteLine("- Train");
        Console.WriteLine("- Metro\n");
    }

    //Utworzenie metody opdowiadajacej za komunikacje miedzyprocesowa (wysyla dane do drugiego procesu) - Punkt w Scorecard: Dwustronna komunikacja miedzyprocesowa
    void Getter()
    {
        string errorFinder;
        string SystemData = $"{directory}SystemData.txt";
        while (!File.Exists(SystemData))
        {
            Thread.Sleep(1000);
        }
        using (var reader = new StreamReader(SystemData))
        {
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------");
            Console.WriteLine(Minutes > 9 ? $"It's {Hour}:{Minutes}" : $"It's {Hour}:0{Minutes}");
            errorFinder = reader.ReadLine();
            if (errorFinder == "Error")
            {
                Console.WriteLine(reader.ReadLine());
            }
            else
            {
                CurrentStop = int.Parse(errorFinder);
                WaitingTime = double.Parse(reader.ReadLine());
                Console.WriteLine($"Your {UserMeansOfTransport} {UserLineNum} is currently at the {CurrentStop} and will arrive in {WaitingTime} min");
                Console.WriteLine("---------------------------------------------------------------------------------------------------------------\n");
            }
        }
        File.Delete(SystemData);
    }

    //Utworzenie metody opdowiadajacej za komunikacje miedzyprocesowa (odbiera dane od drugiego procesu) - Punkt w Scorecard: Dwustronna komunikacja miedzyprocesowa
    void Sender(string UserMeansOfTransport, string UserLineNum, string UserStop, string UserDirection, int Hour, int Minutes)
    {
        string UserData = $"{directory}UserData{iterator}.txt";
        using (var writer = new StreamWriter(UserData))
        {
            writer.WriteLine(UserMeansOfTransport);
            writer.WriteLine(UserLineNum);
            writer.WriteLine(UserStop);
            writer.WriteLine(UserDirection);
            writer.WriteLine(Hour);
            writer.WriteLine(Minutes);
            writer.Flush();
        }
    }

    //Zdefiniowanie funkcji Dispose - Punkt w Scorecard: Implementacja interface IDisposeable
    public void Dispose()
    {
        for (int i = 1; i <= iterator; i++)
        {
            File.Delete($"{directory}UserData{i}.txt");
            Console.WriteLine($"UserData{i}.txt deleted");
        }
    }
}
