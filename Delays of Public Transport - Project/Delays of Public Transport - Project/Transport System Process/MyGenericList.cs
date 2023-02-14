namespace Transport_System_Process;

//Polimorfizm, Dziedziczenie klas - Punkt w Scorecard: Polimorfizm
//Zaprojektowanie typu generycznego - Punkt w Scorecard: Zaprojektowanie typow generycznych
class MyGenericList<T> : MyGenericListPolymorphism<T>
{
    List<T> _items = new List<T>();
    public override List<T> getFileList(string path)
    {
        List<T> _items = new List<T>();
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