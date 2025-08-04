using UnityEngine;
// Clase simple con un �nico comportamiento
public class BreakableBox : MonoBehaviour
{
    
    public AudioClip breakSound;   

    public void Break()
    {
        // Uso de AudioSource est�tico

        if (breakSound != null)
            AudioSource.PlayClipAtPoint(breakSound, transform.position);

        Destroy(gameObject);
    }
}
