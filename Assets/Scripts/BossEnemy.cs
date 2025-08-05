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

    [Header("Detecci�n de suelo")]
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    private bool EstaEnElSuelo()
    {
        

        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

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

        bool haciaDerecha = player.position.x > transform.position.x;
        transform.Translate((haciaDerecha ? Vector2.right : Vector2.left) * moveSpeed * Time.deltaTime);

        MirarHacia(haciaDerecha);

        if (player.position.y > transform.position.y + 1f && EstaEnElSuelo())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerzaSalto);
        }



    }

    private void Disparar()
    {
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootCooldown)
        {
            Vector2 dir = (player.position - firePoint.position).normalized;

            GameObject bala = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            EnemyBullet bullet = bala.GetComponent<EnemyBullet>();
            if (bullet != null)
                bullet.SetDirection(dir);

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
        // Interacción con objetos rompibles

        if (collision.gameObject.CompareTag("BreakableBox"))
        {
            BreakableBox box = collision.gameObject.GetComponent<BreakableBox>();
            if (box != null)
            {
                box.Break();
            }
        }
    }

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

    void LateUpdate()
    {
        float maxAltura = 5f;
        if (transform.position.y > maxAltura)
        {
            Vector3 pos = transform.position;
            pos.y = maxAltura;
            transform.position = pos;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        }
    }



}