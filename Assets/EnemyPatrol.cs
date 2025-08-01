using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float velocidad = 2f;
    public Transform puntoIzquierdo;
    public Transform puntoDerecho;

    private bool moviendoDerecha = true;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (moviendoDerecha)
        {
            rb.linearVelocity = new Vector2(velocidad, rb.linearVelocity.y);
            spriteRenderer.flipX = true;

            if (transform.position.x >= puntoDerecho.position.x)
                moviendoDerecha = false;
        }
        else
        {
            rb.linearVelocity = new Vector2(-velocidad, rb.linearVelocity.y);
            spriteRenderer.flipX = false;

            if (transform.position.x <= puntoIzquierdo.position.x)
                moviendoDerecha = true;
        }
    }
}