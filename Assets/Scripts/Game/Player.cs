using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public class Player : Unit
{
    public event Action<int> OnHealtChanged;

    [SerializeField] private UnityEvent _onDied;
    [SerializeField] private CharacterMovement2D _movement;
    [SerializeField] private Animator _animator;

    private void Start()
    {
        OnHealtChanged?.Invoke(_health);
    }
    private void Update()
    {
        Direction = (MoveDirection)(int)Input.GetAxisRaw("Horizontal");
        _movement.Move(Direction.ToVector2());

        if (Input.GetKeyDown(KeyCode.Space) && _movement.OnGround)
            _movement.Jump();

        UpdateAnimations();
    }

    private void UpdateAnimations()
    {
        _animator.SetBool("run", _movement.Velocity.x != 0);

        if (_movement.OnGround)
            _animator.SetInteger("jump", 0);
        else
            _animator.SetInteger("jump", _movement.Velocity.y > 0 ? 1 : -1);
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Enemy enemy))
        {
            _movement.AddForce(Vector2.up * 250f);
        }
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        OnHealtChanged?.Invoke(_health);
    }

    protected override void Dead()
    {
        _onDied?.Invoke();
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        _movement.Move(Vector2.zero);
        UpdateAnimations();
    }
}


