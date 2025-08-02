using System.Collections.Generic;

public class ObjectList<T>
{
    private List<T> items = new List<T>();

    public void Add(T item) => items.Add(item);
    public void Remove(T item) => items.Remove(item);
    public IEnumerable<T> GetAll() => items;
}
