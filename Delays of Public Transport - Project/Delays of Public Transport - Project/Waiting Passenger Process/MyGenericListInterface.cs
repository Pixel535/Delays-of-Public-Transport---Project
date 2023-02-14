using System.Collections;
namespace Waiting_Passenger_Process;

//Zaprojektowanie typu generycznego - Punkt w Scorecard: Zaprojektowanie typow generycznych
//Utworzenie Interface - Punkt w Scorecard: Kontrakty: Interface
public interface MyGenericListInterface<T>
{
    public void Add(T item);
    public void Remove(T item);
    public bool Contains(T item);
    public int Count();
    public void Dispose();
    public List<T> ReadFromFile(string path);
}