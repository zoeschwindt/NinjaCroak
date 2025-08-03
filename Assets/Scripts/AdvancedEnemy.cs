using UnityEngine;

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

    protected override void Start()
    {
        base.Start(); 
        player = GameObject.FindGameObjectWithTag("Player").transform;
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

    private void Patrullar()
    {
        if (moviendoDerecha)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            if (transform.position.x >= puntoDerecho.position.x)
                moviendoDerecha = false;
        }
        else
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
            if (transform.position.x <= puntoIzquierdo.position.x)
                moviendoDerecha = true;
        }
    }

    private void PerseguirJugador()
    {
        if (player.position.x > transform.position.x)
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        else
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
    }

    private void Disparar()
    {
        temporizadorDisparo += Time.deltaTime;

        if (temporizadorDisparo >= tiempoEntreDisparos)
        {
           
            Vector2 direccion = (player.position - firePoint.position).normalized;

            
            GameObject bala = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

            
            bala.GetComponent<EnemyBullet>().SetDirection(direccion);

            temporizadorDisparo = 0f;
        }
    }

    private void MirarHacia(Vector2 objetivo)
    {
        if (objetivo.x > transform.position.x)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);
    }
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
