using UnityEngine;

public class SimpleEnemy : Enemy
{
    void Update()
    {
        Move();
    }

    protected override void Move()
    {
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
    }
}
