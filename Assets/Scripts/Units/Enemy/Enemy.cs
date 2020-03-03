using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class Enemy : Unit
{
    [SerializeField] protected float _speed;

    protected Collider2D _collider;
    protected Rigidbody2D _body;

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
