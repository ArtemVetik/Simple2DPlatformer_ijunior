using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Player : Unit
{
    [SerializeField] private CharacterMovement2D _movement;
    [SerializeField] private Animator _animator;

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

    protected override void Dead()
    {
        Debug.Log("dead");
    }
}


