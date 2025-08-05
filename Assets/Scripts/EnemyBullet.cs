using UnityEngine;
// Clase para proyectiles enemigos
public class EnemyBullet : MonoBehaviour
{
    public float velocidad = 5f;
    public float tiempoVida = 3f;
    private Vector2 direccion;

    void Start()
    {
        Destroy(gameObject, tiempoVida);
    }
   
    public void SetDirection(Vector2 dir)
    {
        direccion = dir.normalized;

        // Rotación del sprite según dirección
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Update()
    {
        transform.Translate(direccion * velocidad * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si golpea al jugador
        if (other.CompareTag("Player"))
        {
            PlayerHealthUI health = other.GetComponent<PlayerHealthUI>();
            if (health != null)
            {
                health.TakeDamage(1);
            }

            Destroy(gameObject); 
        }
        else if (!other.isTrigger) 
        {
            Destroy(gameObject);
        }
    }
}