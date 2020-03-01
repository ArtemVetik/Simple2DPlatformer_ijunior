using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected int _health;
    [SerializeField] protected float _speed;

    protected event UnityAction die;
    protected int _direction;

    protected void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            _health = 0;
            die?.Invoke();
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
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
