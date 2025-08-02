using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    public int health = 3;

    public virtual void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
