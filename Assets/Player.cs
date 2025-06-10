using UnityEngine;

public class Player : MonoBehaviour
{
    public float velocidadMovimiento = 5f;
    public float fuerzaSalto = 15f;
    public AudioClip sonidoSalto;

    private Rigidbody2D rb;
    private AudioSource audioSource;
    private bool enSuelo = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        float direccion = 0f;

        if (Input.GetKey(KeyCode.A))
            direccion = -1f;
        else if (Input.GetKey(KeyCode.D))
            direccion = 1f;

        MoverHorizontal(direccion);

        if (Input.GetKeyDown(KeyCode.Space))
            Saltar();
    }

    void MoverHorizontal(float direccion)
    {
        Vector2 velocidad = rb.linearVelocity; 
        velocidad.x = direccion * velocidadMovimiento;
        rb.linearVelocity = velocidad;          
    }

    void Saltar()
    {
        if (enSuelo)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerzaSalto); 
            audioSource.PlayOneShot(sonidoSalto);
            enSuelo = false;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Suelo"))
            enSuelo = true;
    }
}
