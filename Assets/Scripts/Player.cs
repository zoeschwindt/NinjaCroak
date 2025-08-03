using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public class Player : Character
{
    public static Action<int> OnLifeChanged;
    public static Action<int> OnScoreChanged;
    public static Action<PowerUp.PowerType, int> OnPowerUpCollected;

    [Header("Movimiento")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public Transform shootPoint;
    public GameObject bulletPrefab;

    [Header("Detección Suelo")]
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private int score;

    [Header("Muerte por caída")]
    public float alturaMuerte = -10f;
    // Estados de control
    private bool canControl = true; 
    private bool isSpeedBoosted = false;
    private bool isJumpBoosted = false;

    private float powerUpDuration = 5f;

    // Doble salto
    private int jumpCount = 0;
    private int maxJumps = 1;

    // Valores base para restaurar stats
    private float baseMoveSpeed;
    private float baseJumpForce;
    private Dictionary<PowerUp.PowerType, int> powerUpInventory = new Dictionary<PowerUp.PowerType, int>();

    private bool wasGrounded = false;

    [Header("Audio")]
    public AudioClip shootSound;       
    private AudioSource audioSource;   

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();

        baseMoveSpeed = moveSpeed;
        baseJumpForce = jumpForce;

        audioSource = gameObject.AddComponent<AudioSource>();

        foreach (PowerUp.PowerType type in Enum.GetValues(typeof(PowerUp.PowerType)))
            powerUpInventory[type] = 0;
    }

    void Update()
    {
        if (!canControl) return; 

        Move();

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        if (Input.GetMouseButtonDown(0))
            Shoot();

        if (Input.GetKeyDown(KeyCode.Q))
            UsePowerUp(PowerUp.PowerType.DoubleJump);

        if (Input.GetKeyDown(KeyCode.E))
            UsePowerUp(PowerUp.PowerType.SpeedBoost);

        if (transform.position.y < alturaMuerte)
        {
            Die();
        }

    }

    void FixedUpdate()
    {
        bool grounded = IsGrounded();

        if (grounded && !wasGrounded)
            jumpCount = 0; 

        wasGrounded = grounded;
    }

    
    public void EnableControl(bool enable)
    {
        canControl = enable;
        if (!enable)
            rb.linearVelocity = Vector2.zero; 
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
    }

    private void Move()
    {
        float move = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(move * moveSpeed, rb.linearVelocity.y);
    }

    private void Jump()
    {
        if (jumpCount < maxJumps)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCount++;
        }
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        if (shootSound != null)
            audioSource.PlayOneShot(shootSound);
    }

    public void AddPowerUp(PowerUp.PowerType type)
    {
        if (!powerUpInventory.ContainsKey(type))
            powerUpInventory[type] = 0;

        powerUpInventory[type]++;
        OnPowerUpCollected?.Invoke(type, powerUpInventory[type]);
    }

    public void UsePowerUp(PowerUp.PowerType type)
    {
        if (powerUpInventory[type] > 0)
        {
            powerUpInventory[type]--;

            OnPowerUpCollected?.Invoke(type, powerUpInventory[type]);

            if (type == PowerUp.PowerType.DoubleJump && !isJumpBoosted)
                StartCoroutine(ApplyJumpBoost());
            else if (type == PowerUp.PowerType.SpeedBoost && !isSpeedBoosted)
                StartCoroutine(ApplySpeedBoost());
        }
    }

    private void ResetStats()
    {
        moveSpeed = baseMoveSpeed;
        jumpForce = baseJumpForce;
        maxJumps = 1;
    }

    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
        GameManager.Instance.LoseLife(amount); // Descuenta vida en GameManager
        OnLifeChanged?.Invoke(currentHealth);
    }

    public void AddScore(int amount)
    {
        score += amount;
        GameManager.Instance.AddScore(amount); // Suma puntaje en GameManager
        OnScoreChanged?.Invoke(score);
    }

    private IEnumerator ApplySpeedBoost()
    {
        isSpeedBoosted = true;
        moveSpeed = baseMoveSpeed * 1.5f;

        yield return new WaitForSeconds(powerUpDuration);

        moveSpeed = baseMoveSpeed;
        isSpeedBoosted = false;
    }

    private IEnumerator ApplyJumpBoost()
    {
        isJumpBoosted = true;
        jumpForce = baseJumpForce * 1.2f;
        maxJumps = 2;

        yield return new WaitForSeconds(powerUpDuration);

        jumpForce = baseJumpForce;
        maxJumps = 1;
        isJumpBoosted = false;
    }

    protected override void Die()
    {
        Debug.Log("Jugador muerto por caída");

        // Guarda escena actual antes de morir
        GameSession.LastSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Moriste");
    }
}
