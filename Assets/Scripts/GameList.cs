using System.Collections.Generic;

public class GameList<T>
{
    private List<T> items = new List<T>();

    public void Add(T item) => items.Add(item); 
    public void Remove(T item) => items.Remove(item); 
    public List<T> GetAll() => new List<T>(items); // Devuelve una copia de la lista
    public int Count => items.Count; // Propiedad de solo lectura para contar elementos
}
