using UnityEngine;
// Clase simple con un único comportamiento
public class BreakableBox : MonoBehaviour
{
    
    public AudioClip breakSound;   

    public void Break()
    {
        // Uso de AudioSource estático

        if (breakSound != null)
            AudioSource.PlayClipAtPoint(breakSound, transform.position);

        Destroy(gameObject);
    }
}
