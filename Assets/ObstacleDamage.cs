using UnityEngine;

public class ObstacleDamage : MonoBehaviour
{
    public int damageAmount = 2; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealthUI health = other.GetComponent<PlayerHealthUI>();
            if (health != null)
            {
                health.TakeDamage(damageAmount);
            }
        }
    }
}