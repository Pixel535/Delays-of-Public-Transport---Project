// See https://aka.ms/new-console-template for more information

using S = Waiting_Passenger_Process.UserInput;

S start = new S();
string Command;

string directory = Directory.GetCurrentDirectory().Substring(0, Directory.GetCurrentDirectory().Length - 42);

while (true)
{
    Console.WriteLine("\nWelcome to the programme with which you can check whether your public transport will be on time or not !");
    Console.WriteLine("---------------------------------------------------------------------------------------------------------------");
    Console.WriteLine("[0] - Exit");
    Console.WriteLine("[1] - Start Programme");
    Console.WriteLine("---------------------------------------------------------------------------------------------------------------\n");
    Console.WriteLine("Picked Command: ");
    Command = Console.ReadLine();
    switch (Command)
    {
        //Komunikat o wylaczeniu procesu - Punkt w Scorecard: Dwustronna komunikacja miedzyprocesowa
        case "0":
            string EndDataB = $"{directory}EndDataA.txt";
            using (var writer = new StreamWriter(EndDataB))
            {
                writer.WriteLine("END");
                writer.Flush();
            }
            string EndDataM = $"{directory}EndDataM.txt";
            using (var writer = new StreamWriter(EndDataM))
            {
                writer.WriteLine("END");
                writer.Flush();
            }
            string EndDataP = $"{directory}EndDataP.txt";
            using (var writer = new StreamWriter(EndDataP))
            {
                writer.WriteLine("END");
                writer.Flush();
            }
            string EndDataT = $"{directory}EndDataT.txt";
            using (var writer = new StreamWriter(EndDataT))
            {
                writer.WriteLine("END");
                writer.Flush();
            }
            //Wywolanie metody Dispose - Punkt w Scorecard: Implementacja interface'u IDisposeable
            start.Dispose();
            return 0;
            break;
        case "1": start.iterator += 1; start.GetData(); Console.ReadLine(); break;
    }
}