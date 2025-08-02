using UnityEngine;

public class Collectible : MonoBehaviour, ICollectible
{
    public void Collect()
    {
        GameManager.Instance.AddCollectible();
        Destroy(gameObject);
    }

    private void Start()
    {
        GameManager.Instance.RegisterCollectible(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            Collect();
    }
}