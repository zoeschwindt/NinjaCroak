using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.TextCore.Text;
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

    private Rigidbody2D rb;
    private int score;

    private bool isSpeedBoosted = false;
    private bool isJumpBoosted = false;

    private float powerUpDuration = 5f;

    private bool canDoubleJump = false;
    private int jumpCount = 0;
    private int maxJumps = 1;

    private float baseMoveSpeed;
    private float baseJumpForce;
    private Dictionary<PowerUp.PowerType, int> powerUpInventory = new Dictionary<PowerUp.PowerType, int>();

    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask groundLayer;




    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();

       
        baseMoveSpeed = moveSpeed;
        baseJumpForce = jumpForce;

        
        foreach (PowerUp.PowerType type in Enum.GetValues(typeof(PowerUp.PowerType)))
            powerUpInventory[type] = 0;
    }

    private bool wasGrounded = false;

void Update()
{
    Move();

    if (Input.GetKeyDown(KeyCode.Space))
        Jump();

    if (Input.GetMouseButtonDown(0))
        Shoot();

    if (Input.GetKeyDown(KeyCode.Q))      // Q para DoubleJump
        UsePowerUp(PowerUp.PowerType.DoubleJump);

    if (Input.GetKeyDown(KeyCode.E))      // E para SpeedBoost
        UsePowerUp(PowerUp.PowerType.SpeedBoost);
}

    void FixedUpdate()
    {
        bool grounded = IsGrounded();

       
        if (grounded && !wasGrounded)
        {
            jumpCount = 0;
        }

        wasGrounded = grounded;
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
        jumpForce = baseJumpForce * 1f;
        maxJumps = 2;

        yield return new WaitForSeconds(powerUpDuration);

        jumpForce = baseJumpForce;
        maxJumps = 1;
        isJumpBoosted = false;
    }

    protected override void Die()
    {
        Debug.Log("Jugador muerto");
    }
}
