using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 20;

    private Vector2 direction = Vector2.right;

    // M�todo para establecer la direcci�n del disparo
    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;

        // Ajusta la escala para que la bala mire hacia la direcci�n correcta (visual)
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * (dir.x < 0 ? -1 : 1);
        transform.localScale = scale;
    }

    void Start()
    {
        Destroy(gameObject, 1f); // Destruye la bala despu�s de 1 segundo para no acumular objetos
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Si el objeto colisionado implementa la interfaz IDamageable, le aplica da�o
        if (other.TryGetComponent<IDamageable>(out var target))
        {
            target.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

}
