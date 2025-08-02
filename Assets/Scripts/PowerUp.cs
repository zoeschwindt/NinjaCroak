using UnityEngine;

public class PowerUp : MonoBehaviour, ICollectible
{
    public enum PowerType { DoubleJump, SpeedBoost }
    public PowerType powerType;

    [System.Obsolete]
    public void Collect()
    {
        Player player = FindObjectOfType<Player>();

        if (powerType == PowerType.DoubleJump)
            player.jumpForce *= 1.5f;
        else if (powerType == PowerType.SpeedBoost)
            player.moveSpeed *= 1.5f;

        Destroy(gameObject);
    }

    [System.Obsolete]
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            Collect();
    }
}
