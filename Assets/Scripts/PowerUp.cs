using UnityEngine;

public class PowerUp : MonoBehaviour, ICollectible
{
    public enum PowerType { DoubleJump, SpeedBoost }
    public PowerType powerType;

    public void Collect()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            Player player = playerObj.GetComponent<Player>();
            if (player != null)
            {
                player.AddPowerUp(powerType);
                Destroy(gameObject);
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
