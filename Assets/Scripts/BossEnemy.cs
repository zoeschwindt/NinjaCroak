using UnityEngine;

public class BossEnemy : Enemy
{
    [Header("Movimiento")]
    public float rangoDeteccion = 8f;
    public float fuerzaSalto = 5f;

    [Header("Ataque a distancia")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float shootCooldown = 1.5f;
    private float shootTimer;

    private Transform player;
    private Rigidbody2D rb;

    protected override void Start()
    {
        base.Start(); 
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (player == null) return;

        float distancia = Vector2.Distance(transform.position, player.position);

        if (distancia <= rangoDeteccion)
        {
            PerseguirJugador();
            Disparar();
        }
    }

    private void PerseguirJugador()
    {
        if (player.position.x > transform.position.x)
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        else
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

        
        if (player.position.y > transform.position.y + 1f && Mathf.Abs(rb.linearVelocity.y) < 0.1f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerzaSalto);
        }
    }

    private void Disparar()
    {
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootCooldown)
        {
            Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            shootTimer = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealthUI health = collision.gameObject.GetComponent<PlayerHealthUI>();
            if (health != null)
            {
                health.TakeDamage(2); 
            }
        }

        
        if (collision.gameObject.CompareTag("BreakableBox"))
        {
            BreakableBox box = collision.gameObject.GetComponent<BreakableBox>();
            if (box != null)
            {
                box.Break();
            }
        }
    }
}
