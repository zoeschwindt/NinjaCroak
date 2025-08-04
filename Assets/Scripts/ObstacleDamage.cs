using UnityEngine;


//Aplica da�o al jugador cuando entra en contacto con un obst�culo.
public class ObstacleDamage : MonoBehaviour
{
    // Encapsulaci�n: variable p�blica para configurar da�o

    public int damageAmount = 2; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Detecta si el objeto que colisiona es el jugador

        if (other.CompareTag("Player"))
        {
            // Obtiene el componente PlayerHealthUI para aplicar da�o
            PlayerHealthUI health = other.GetComponent<PlayerHealthUI>();
            if (health != null)
            {
                health.TakeDamage(damageAmount);
            }
        }
    }
}