using UnityEngine;
using System;
// Clase  que usa eventos para notificar caída del jugador
public class PlayerFallHandler : MonoBehaviour
{
    public float fallThreshold = -10f;
    public static event Action OnPlayerFall; 

    void Update()
    {
        if (transform.position.y < fallThreshold)
        {
            OnPlayerFall?.Invoke(); 
        }
    }
}
