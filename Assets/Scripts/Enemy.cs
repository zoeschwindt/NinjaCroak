using UnityEngine;

public abstract class Enemy : Character
{
    public float moveSpeed = 2f;

    protected virtual void Move() { }

    protected override void Die()
    {
        Destroy(gameObject);
    }
}
