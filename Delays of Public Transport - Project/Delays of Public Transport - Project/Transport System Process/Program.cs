// See https://aka.ms/new-console-template for more information

using Transport_System_Process;
using B = Transport_System_Process.Bus;
using M = Transport_System_Process.Metro;
using P = Transport_System_Process.Train;
using T = Transport_System_Process.Tram;

B Bus = new B();
M Metro = new M();
P Train = new P();
T Tram = new T();

//Oczekiwanie na zakonczenie wszystkich asynchronicznych taskow - Punkt w Scorecard: Metody async, synchronizacja await
await Task.WhenAll(Bus.FileReader(), Metro.FileReader(), Train.FileReader(), Tram.FileReader());

//Wykorzystanie typu generycznego i kowarjancji - Punkt w Scorecard: Zaprojektowanie typu generycznego i kowariancja
MyGenericList<MeansOfTransport> meansOfTransport = new MyGenericList<MeansOfTransport>{Bus,Metro,Train,Tram};
while (!Bus.End && !Metro.End && !Train.End && !Tram.End)
{
    foreach (var meanOfTransport in meansOfTransport)
    {
        meanOfTransport.getIterator += 1;
    }
    
    //Oczekiwanie na zakonczenie wszystkich asynchronicznych taskow - Punkt w Scorecard: Metody async, synchronizacja await
    await Task.WhenAll(Bus.Getter(), Metro.Getter(), Train.Getter(), Tram.Getter());
}
