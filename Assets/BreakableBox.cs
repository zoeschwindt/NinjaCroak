using UnityEngine;

public class BreakableBox : MonoBehaviour
{
    public GameObject breakEffect; 
    public AudioClip breakSound;   

    public void Break()
    {
        if (breakEffect != null)
            Instantiate(breakEffect, transform.position, Quaternion.identity);

        if (breakSound != null)
            AudioSource.PlayClipAtPoint(breakSound, transform.position);

        Destroy(gameObject);
    }
}
