using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    public float moveSpeed = 2f;
    public int health = 3;

    protected virtual void Start()
    {
        
        GameManager.Instance.RegisterEnemy(this);
    }

    public virtual void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            
            GameManager.Instance.AddDefeatedEnemy();
            Destroy(gameObject);
        }
    }
}
