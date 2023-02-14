namespace Transport_System_Process;

//Utworzenie Interface - Punkt w Scorecard: Kontrakty: Interface
//Zastosowanie interface IDisposeable - Punkt w Scorecard: Implementacja interface'u IDisposeable
public interface MeansOfTransport : IDisposable
{
    Task FileReader();
    void CalculationOfDelay();
    Task Getter();
    void Sender(string CurrentStop, double WaitingTime, bool toEarly);

    int getIterator { get; set; }
}