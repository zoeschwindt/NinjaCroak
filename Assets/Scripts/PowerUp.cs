using UnityEngine;
// Implementa la interfaz ICollectible (cumple polimorfismo)
public class PowerUp : MonoBehaviour, ICollectible
{
    public enum PowerType { DoubleJump, SpeedBoost }
    public PowerType powerType;

    // Implementación del método de la interfaz
    public void Collect()
    {
        // Busca al jugador y le añade el power-up correspondiente
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            Player player = playerObj.GetComponent<Player>();
            if (player != null)
            {
                player.AddPowerUp(powerType);
                Destroy(gameObject); // Se destruye el power-up al ser recogido
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Collect();
        }
    }
}
