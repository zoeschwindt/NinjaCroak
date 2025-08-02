using UnityEngine;

public class SimpleEnemy : Enemy
{
    void Update()
    {
        Move();
    }

    protected override void Move()
    {
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealthUI health = collision.gameObject.GetComponent<PlayerHealthUI>();
            if (health != null)
            {
                health.TakeDamage(1);
            }
        }
    }
}
