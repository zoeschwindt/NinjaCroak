using UnityEngine;


//Aplica daño al jugador cuando entra en contacto con un obstáculo.
public class ObstacleDamage : MonoBehaviour
{
    // Encapsulación: variable pública para configurar daño

    public int damageAmount = 2; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Detecta si el objeto que colisiona es el jugador

        if (other.CompareTag("Player"))
        {
            // Obtiene el componente PlayerHealthUI para aplicar daño
            PlayerHealthUI health = other.GetComponent<PlayerHealthUI>();
            if (health != null)
            {
                health.TakeDamage(damageAmount);
            }
        }
    }
}