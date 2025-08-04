using UnityEngine;


// Herencia: AdvancedEnemy hereda de Enemy (comportamiento base de enemigos)
public class AdvancedEnemy : Enemy
{
    [Header("Patrullaje")]
    public Transform puntoIzquierdo;
    public Transform puntoDerecho;
    private bool moviendoDerecha = true;

    [Header("Persecución")]
    public float rangoPersecucion = 5f;
    private Transform player;

    [Header("Ataque a distancia")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float rangoDisparo = 3f;
    public float tiempoEntreDisparos = 2f;
    private float temporizadorDisparo;

    private Vector3 escalaOriginal;

    protected override void Start()
    {
        base.Start();  // Polimorfismo: llama al Start() definido en Enemy
        player = GameObject.FindGameObjectWithTag("Player").transform;
        escalaOriginal = new Vector3(
            Mathf.Abs(transform.localScale.x),
            transform.localScale.y,
            transform.localScale.z
        );
    }

    void Update()
    {
        float distanciaJugador = Vector2.Distance(transform.position, player.position);

        if (distanciaJugador <= rangoPersecucion)
        {
            PerseguirJugador();

            if (distanciaJugador <= rangoDisparo)
            {
                Disparar();
            }
        }
        else
        {
            Patrullar();
        }
    }
    //  Método propio (patrullaje) sin retorno
    private void Patrullar()
    {
        if (moviendoDerecha)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            MirarHacia(true);

            if (transform.position.x >= puntoDerecho.position.x)
                moviendoDerecha = false;
        }
        else
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            MirarHacia(false);

            if (transform.position.x <= puntoIzquierdo.position.x)
                moviendoDerecha = true;
        }
    }
    // Método propio (perseguir jugador)
    private void PerseguirJugador()
    {
        bool jugadorADerecha = player.position.x > transform.position.x;

        if (jugadorADerecha)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        }

        MirarHacia(jugadorADerecha);
    }

    // Método con temporizador: controla frecuencia de disparo
    private void Disparar()
    {
        temporizadorDisparo += Time.deltaTime;

        if (temporizadorDisparo >= tiempoEntreDisparos)
        {
            Vector2 direccion = (player.position - firePoint.position).normalized;

            //  Instanciación de objeto (bala) en runtime
            GameObject bala = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            bala.GetComponent<EnemyBullet>().SetDirection(direccion);

            temporizadorDisparo = 0f;
        }
    }

    // Método reutilizable para orientar sprite y punto de disparo
    private void MirarHacia(bool haciaDerecha)
    {
        SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();
        if (sr != null)
            sr.flipX = haciaDerecha; 

        Vector3 pos = firePoint.localPosition;
        float distancia = Mathf.Abs(pos.x);

        pos.x = haciaDerecha ? distancia : -distancia;
        firePoint.localPosition = pos;
    }



    // Colisión con el jugador
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealthUI health = collision.gameObject.GetComponent<PlayerHealthUI>();
            if (health != null)
            {
                health.TakeDamage(1);
            }
        }
    }
}
