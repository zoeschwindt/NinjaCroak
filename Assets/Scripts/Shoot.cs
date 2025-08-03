using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 20;

    void Start()
    {
        Destroy(gameObject, 1f);
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IDamageable>(out var target))
        {
            target.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
