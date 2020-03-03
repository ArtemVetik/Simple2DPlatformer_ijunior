using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LandMonster : Enemy
{
    [SerializeField] private float _closeDistance;
    [SerializeField] [Range(0f, 1f)] private float _groundNormal;

    private Animator _animator;
    private LayerMask _playerLayer;
    private float? _lastHitFraction;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
        _body = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _body.freezeRotation = true;
        _lastHitFraction = null;
        _playerLayer = LayerMask.NameToLayer("Player") << 8;

        Direction = MoveDirection.Right;
        _animator.SetBool("run", _speed != 0);
    }

    private void Update()
    {
        _body.velocity = new Vector2(_speed * (int)Direction, _body.velocity.y);

        if (CanMove(_collider, Direction, _closeDistance) == false)
        {
            _lastHitFraction = null;
            Direction = Direction.Flip();
        }
    }

    private bool CanMove(Collider2D collider, MoveDirection direction, float stopDistance)
    {
        float xPos = direction > 0 ? collider.bounds.max.x + 0.01f : collider.bounds.min.x - 0.01f;
        Vector2 origin = new Vector2(xPos + stopDistance * (int)direction, collider.bounds.center.y);
        RaycastHit2D hitDown = Physics2D.Raycast(origin, Vector2.down, Mathf.Infinity, _playerLayer);

        if (_lastHitFraction == null)
            _lastHitFraction = hitDown.fraction;

        return hitDown.normal.y >= _groundNormal && Mathf.Abs(hitDown.fraction - _lastHitFraction.Value) < 1f;
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (var contact in collision.contacts)
        {
            if (contact.normal.y < _groundNormal)
            {
                Direction = Direction.Flip();
                break;
            }
        }
        base.OnCollisionEnter2D(collision);
    }
}
