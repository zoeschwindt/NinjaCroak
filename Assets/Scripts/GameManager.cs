using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static Action OnLevelComplete;

    private List<Enemy> enemies = new List<Enemy>();

    [Obsolete]
    void Awake()
    {
        Instance = this;
        enemies = FindObjectsOfType<Enemy>().ToList();
    }

    public void CheckEnemies()
    {
        if (enemies.All(e => e == null))
            OnLevelComplete?.Invoke();
    }

    // Ejemplo de uso de LINQ para encontrar enemigo más cercano
    public Enemy GetClosestEnemy(Vector3 position)
    {
        return enemies
            .Where(e => e != null)
            .OrderBy(e => Vector3.Distance(position, e.transform.position))
            .FirstOrDefault();
    }
}
