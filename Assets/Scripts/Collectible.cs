using UnityEngine;

public class Collectible : MonoBehaviour, ICollectible
{
    public int points = 10;

    [System.Obsolete]
    public void Collect()
    {
        FindObjectOfType<Player>().AddScore(points);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            Collect();
    }
}
