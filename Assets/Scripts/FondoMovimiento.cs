using UnityEngine;

public class FondoMovimiento : MonoBehaviour
{
    // Velocidad con la que se mueve el fondo

    [SerializeField] private Vector2 velocidadMovimiento;

    private Vector2 offset;

    private Material material;

    private Rigidbody2D jugadorRB;

    private void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
        jugadorRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>(); 
    }

    private void Update()
    {
        // Calcular el desplazamiento del fondo en función del movimiento del jugador
        offset = (jugadorRB.linearVelocity.x * 0.1f) * velocidadMovimiento * Time.deltaTime;
        material.mainTextureOffset += offset;
    }



}
