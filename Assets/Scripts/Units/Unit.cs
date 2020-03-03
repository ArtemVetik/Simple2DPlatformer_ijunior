using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveDirection : int
{
    Right = 1, Left = -1, None = 0,
}

[RequireComponent(typeof(SpriteRenderer))]
abstract public class Unit : MonoBehaviour
{
    [SerializeField] protected int _health;

    protected SpriteRenderer _sprite;

    private MoveDirection _direction;

    protected MoveDirection Direction
    {
        get
        {
            return _direction;
        }
        set
        {
            _direction = value;
            FlipSprite();
        }
    }

    public virtual void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            _health = 0;
            Dead();
        }
    }

    protected virtual void Dead()
    {
        Destroy(gameObject);
    }

    protected void FlipSprite()
    {
        if (_direction != MoveDirection.None)
            _sprite.flipX = _direction < 0;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision) { }
}
