using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Linq;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public event Action<int, int> OnCollectiblesChanged; 
    public event Action<int, int> OnEnemiesChanged;      
    public event Action<int> OnLivesChanged;             
    public event Action<int> OnScoreChanged;             

    [Header("Objetivos del Nivel")]
    public int totalCollectibles = 5;
    public int totalEnemies = 12;

    [Header("Jugador")]
    public int maxLives = 4;

    private int collectedCount = 0;
    private int defeatedEnemies = 0;
    private int currentLives;
    private int score;

    // Listas genéricas
    private GameList<ICollectible> collectibles = new GameList<ICollectible>();
    private GameList<IDamageable> enemies = new GameList<IDamageable>();

    void Awake()
    {
        if (Instance == null) Instance = this;
        currentLives = maxLives;
    }

    //COLECCIONABLES
    public void RegisterCollectible(ICollectible collectible)
    {
        collectibles.Add(collectible);
    }

    public void AddCollectible()
    {
        collectedCount++;
        OnCollectiblesChanged?.Invoke(collectedCount, totalCollectibles);
        CheckLevelComplete();
    }

    //ENEMIGOS
    public void RegisterEnemy(IDamageable enemy)
    {
        enemies.Add(enemy);
    }

    public void AddDefeatedEnemy()
    {
        defeatedEnemies++;
        OnEnemiesChanged?.Invoke(defeatedEnemies, totalEnemies);
        CheckLevelComplete();
    }

    // VIDAS 
    public void LoseLife(int amount)
    {
        currentLives -= amount;
        OnLivesChanged?.Invoke(currentLives);

        if (currentLives <= 0)
        {
            SceneManager.LoadScene("Moriste");
        }
    }

    public int GetLives() => currentLives;

    // PUNTAJE
    public void AddScore(int amount)
    {
        score += amount;
        OnScoreChanged?.Invoke(score);
    }

    public int GetScore() => score;

    // CHEQUEAR FIN DE NIVEL 
    private void CheckLevelComplete()
    {
        if (collectedCount >= totalCollectibles && defeatedEnemies >= totalEnemies)
        {
            SceneManager.LoadScene("Nivel2");
        }
    }
}