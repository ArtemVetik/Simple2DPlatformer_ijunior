using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveDirection : int
{
    Right = 1, Left = -1, None = 0,
}
public class Unit : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer _sprite;
    [SerializeField] protected int _health;

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
            if (_direction != MoveDirection.None)
                _sprite.flipX = (int)_direction < 0;
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

    protected virtual void OnCollisionEnter2D(Collision2D collision) { }
}
