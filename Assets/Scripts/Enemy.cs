using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : Unit
{
    [SerializeField] protected Collider2D _collider;
    [SerializeField] protected float _speed;

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            foreach (var contact in collision.contacts)
            {
                if (contact.point.y > transform.position.y + 0.25f * transform.localScale.y)
                {
                    TakeDamage(1);
                    return;
                }
            }
            player.TakeDamage(1);
        }
    }
}
