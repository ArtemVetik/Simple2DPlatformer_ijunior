using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private CharacterMovement2D _movement;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Animator _animator;

    private void Update()
    {
        Vector2 moveDir = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
        _movement.Move(moveDir);

        if (Input.GetKeyDown(KeyCode.Space) && _movement.OnGround)
            _movement.Jump();

        UpdateAnimations();
        FlipSprite(moveDir);
    }

    private void UpdateAnimations()
    {
        _animator.SetBool("run", _movement.Velocity.x != 0);

        if (_movement.OnGround)
            _animator.SetInteger("jump", 0);
        else
            _animator.SetInteger("jump", _movement.Velocity.y > 0 ? 1 : -1);
    }

    private void FlipSprite(Vector2 moveDirection)
    {
        _sprite.flipX = moveDirection.x < 0 ? true : moveDirection.x > 0 ? false : _sprite.flipX;

    }
}


