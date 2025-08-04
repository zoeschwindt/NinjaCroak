using System.Collections.Generic;
// Clase genérica que puede contener cualquier tipo de objeto (T)
public class GameList<T>
{
    private List<T> items = new List<T>();

    public void Add(T item) => items.Add(item); // Agrega un elemento
    public void Remove(T item) => items.Remove(item); // Elimina un elemento
    public List<T> GetAll() => new List<T>(items); // Devuelve una copia de la lista
    public int Count => items.Count; // Propiedad de solo lectura para contar elementos
}
