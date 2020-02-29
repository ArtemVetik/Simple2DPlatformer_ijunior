using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private CharacterMovement2D _movement;
    [SerializeField] private SpriteRenderer _sprite;

    private void Update()
    {
        Vector2 moveDir = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
        _movement.Move(moveDir);

        if (Input.GetKey(KeyCode.Space) && _movement.OnGround)
            _movement.Jump();

        FlipSprite(moveDir);
    }

    private void FlipSprite(Vector2 moveDirection)
    {
        _sprite.flipX = moveDirection.x < 0 ? true : moveDirection.x > 0 ? false : _sprite.flipX;

    }
}


