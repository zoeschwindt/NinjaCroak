using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 20;

    private Vector2 direction = Vector2.right;

    
    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;

        
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * (dir.x < 0 ? -1 : 1);
        transform.localScale = scale;
    }

    void Start()
    {
        Destroy(gameObject, 1f); 
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
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
