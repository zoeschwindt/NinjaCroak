using UnityEngine;

public class BossEnemy : Enemy
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float shootCooldown = 2f;
    private float shootTimer;

    void Update()
    {
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootCooldown)
        {
            Shoot();
            shootTimer = 0f;
        }
    }

    private void Shoot()
    {
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealthUI health = collision.gameObject.GetComponent<PlayerHealthUI>();
            if (health != null)
            {
                health.TakeDamage(2);
            }
        }
    }
}
