namespace Waiting_Passenger_Process;

//Zaprojektowanie typu generycznego - Punkt w Scorecard: Zaprojektowanie typow generycznych
//Wykorzystanie Interface - Punkt w Scorecard: Kontrakty: Interface
class MyGenericList<T> : MyGenericListInterface<T>
{
    private List<T> _items = new List<T>();
    public void Add(T item)
    {
        _items.Add(item);
    }

    public void Remove(T item)
    {
        _items.Remove(item);
    }

    public bool Contains(T item)
    {
        return _items.Contains(item);
    }

    public int Count()
    {
        return _items.Count;
    }

    public void Dispose()
    {
        _items = null;
    }

    public List<T> ReadFromFile(string path)
    {
        using (var file = File.OpenText(path))
        {
            string line;
            while ((line = file.ReadLine()) != null)
            {
                T item = (T)Convert.ChangeType(line, typeof(T));
                _items.Add(item);
            }
        }
        return _items;
    }
}