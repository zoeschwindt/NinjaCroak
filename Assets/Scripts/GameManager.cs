using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static Action OnLevelComplete;
    public event Action<int> OnScoreChanged; 

    private List<Enemy> enemies = new List<Enemy>();
    private int score;

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

    public Enemy GetClosestEnemy(Vector3 position)
    {
        return enemies
            .Where(e => e != null)
            .OrderBy(e => Vector3.Distance(position, e.transform.position))
            .FirstOrDefault();
    }

    public void AddScore(int amount)
    {
        score += amount;
        OnScoreChanged?.Invoke(score); 
    }

    public int GetScore()
    {
        return score;
    }
}
