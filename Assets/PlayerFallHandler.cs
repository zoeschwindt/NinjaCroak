using UnityEngine;
using System;

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
