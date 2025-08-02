using UnityEngine;

public class Collectible : MonoBehaviour, ICollectible
{
    public int points = 1;

    public void Collect()
    {
        GameManager.Instance.AddScore(points);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            Collect();
    }
}
