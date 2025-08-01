using UnityEngine;
using System;
using UnityEngine.TextCore.Text;

public class Player : Character
{
    public static Action<int> OnLifeChanged;
    public static Action<int> OnScoreChanged;

    [Header("Movimiento")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public Transform shootPoint;
    public GameObject bulletPrefab;

    private Rigidbody2D rb;
    private int score;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
        if (Input.GetMouseButtonDown(0))
            Shoot();
    }

    private void Move()
    {
        float move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
    }

    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
        OnLifeChanged?.Invoke(currentHealth);
    }

    public void AddScore(int amount)
    {
        score += amount;
        OnScoreChanged?.Invoke(score);
    }

    protected override void Die()
    {
        Debug.Log("Jugador muerto");
        
    }
}
