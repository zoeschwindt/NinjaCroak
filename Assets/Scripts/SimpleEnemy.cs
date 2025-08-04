using UnityEngine;

public class SimpleEnemy : Enemy
{
    public Transform puntoIzquierdo;
    public Transform puntoDerecho;
    private bool moviendoDerecha = true;
    private Vector3 escalaOriginal;

    protected override void Start()
    {
        escalaOriginal = transform.localScale;
    }

    void Update()
    {
        Patrullar();
    }
    // Método privado para controlar el movimiento de patrulla entre dos puntos
    private void Patrullar()
    {
        if (moviendoDerecha)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            transform.localScale = new Vector3(Mathf.Abs(escalaOriginal.x), escalaOriginal.y, escalaOriginal.z);  // Escala para que el sprite mire hacia la derecha

            if (transform.position.x >= puntoDerecho.position.x)
                moviendoDerecha = false;
        }
        else
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            transform.localScale = new Vector3(-Mathf.Abs(escalaOriginal.x), escalaOriginal.y, escalaOriginal.z); // Escala para que el sprite mire hacia la izquierda

            if (transform.position.x <= puntoIzquierdo.position.x)
                moviendoDerecha = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si colisiona con el jugador, le quita vida a través del PlayerHealthUI
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealthUI health = collision.gameObject.GetComponent<PlayerHealthUI>();
            if (health != null) health.TakeDamage(1);
        }
    }
}
