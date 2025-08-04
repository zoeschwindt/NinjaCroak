using UnityEngine;


// Clase abstracta (no se instancia directamente)
// Implementa interfaz IDamageable
public abstract class Character : MonoBehaviour, IDamageable
{
    [SerializeField] protected int maxHealth = 100; //  Encapsulamiento: protected
    protected int currentHealth;

    protected virtual void Awake()
    {
        currentHealth = maxHealth;
    }
    //  Método de interfaz con implementación por defecto
    public virtual void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
            Die(); // Polimorfismo: se implementa diferente en subclases
    }

    protected abstract void Die(); // Obliga a las subclases a definir su muerte
}
