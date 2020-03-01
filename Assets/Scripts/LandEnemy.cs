using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class LandEnemy : Enemy
{
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _stopDistance;
    [SerializeField] [Range(0f, 1f)] private float _groundNormal;

    private Rigidbody2D _body;
    private float? _lastHitFraction;
    private LayerMask _layer;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        _body.freezeRotation = true;
        _lastHitFraction = null;
        _layer = LayerMask.NameToLayer("Player") << 8;

        Flip(_direction = 1);
        _animator.SetBool("run", true);

        die += () => Destroy(gameObject); 
    }

    private void Update()
    {
        _body.velocity = new Vector2(_speed * _direction, _body.velocity.y);

        if (CanMove(_collider, _direction, _stopDistance) == false)
        {
            _lastHitFraction = null;
            _direction *= -1;
            Flip(_direction);
        }
    }

    private bool CanMove(Collider2D collider, int direction, float stopDistance)
    {
        float xPos = direction > 0 ? collider.bounds.max.x + 0.01f : collider.bounds.min.x - 0.01f;
        Vector2 origin = new Vector2(xPos + stopDistance * direction, collider.bounds.center.y);
        RaycastHit2D hitDown = Physics2D.Raycast(origin, Vector2.down, Mathf.Infinity, _layer);
        RaycastHit2D hitDirection = Physics2D.Raycast(new Vector2(xPos, collider.bounds.center.y), Vector2.right * direction, stopDistance, _layer);

        if (_lastHitFraction == null)
            _lastHitFraction = hitDown.fraction;

        return hitDown.normal.y >= _groundNormal && /*hitDirection.collider == null &&*/ Mathf.Abs(hitDown.fraction - _lastHitFraction.Value) < 1f;
    }

    private void Flip(int direction)
    {
        _sprite.flipX = direction > 0;
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (var contact in collision.contacts)
        {
            if (contact.normal.y < _groundNormal)
            {
                _direction *= -1;
                Flip(_direction);
                break;
            }
        }
        base.OnCollisionEnter2D(collision);
    }
}
