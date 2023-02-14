namespace Transport_System_Process;

//Zaprojektowanie typu generycznego - Punkt w Scorecard: Zaprojektowanie typow generycznych
//Polimorfizm, Dziedziczenie klas, stworzenie klasy maciezystej - Punkt w Scorecard: Polimorfizm
public class MyGenericListPolymorphism<T>: List<T>
{
    private List<T> _items = new List<T>();
    public virtual List<T> getFileList(string path) { return _items;}
}