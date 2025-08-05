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

    [Header("Transición de Nivel")]
    public string nextSceneName = "Nivel2";

    

    private int collectedCount = 0;
    private int defeatedEnemies = 0;
    private int currentLives;
    private int score;

    
    private GameList<ICollectible> collectibles = new GameList<ICollectible>();
    private GameList<IDamageable> enemies = new GameList<IDamageable>();


    void Start()
    {
        var masCercano = GameManager.Instance.GetClosestEnemyToPlayer(transform);
        if (masCercano != null)
            Debug.Log("Enemigo más cercano: " + masCercano.name);
    }


        public Enemy GetClosestEnemyToPlayer(Transform player)
    {
        // LINQ + Lambda: Ordena los enemigos según la distancia y devuelve el más cercano.
        return enemies.GetAll()
            .OfType<Enemy>() // Convierte la lista a solo enemigos (por si hay otros IDamageable).
            .OrderBy(enemy => Vector2.Distance(enemy.transform.position, player.position)) // LAMBDA
            .FirstOrDefault();
    }


    public Collectible GetClosestCollectibleToPlayer(Transform player)
    {
        // LINQ + lambda: busca el coleccionable más cercano que siga existiendo
        return collectibles.GetAll()
            .OfType<Collectible>()
            .Where(collectible => collectible != null) // <-- filtra sólo los que existen
            .OrderBy(collectible => Vector2.Distance(collectible.transform.position, player.position))
            .FirstOrDefault();
    }



    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        currentLives = maxLives;
    }

    // COLECCIONABLES
    public void RegisterCollectible(ICollectible collectible)
    {
        collectibles.Add(collectible);// Agrega coleccionable a la lista
    }

    public void AddCollectible()
    {
        collectedCount++;
        OnCollectiblesChanged?.Invoke(collectedCount, totalCollectibles);
        CheckLevelComplete();
    }

    // ENEMIGOS
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
            GameSession.LastSceneName = SceneManager.GetActiveScene().name;
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
            if (!string.IsNullOrEmpty(nextSceneName))
            {
                SceneManager.LoadScene(nextSceneName);
            }
            else
            {
                Debug.LogWarning("Nombre de escena siguiente no asignado en el GameManager.");
            }
        }
    }
}
