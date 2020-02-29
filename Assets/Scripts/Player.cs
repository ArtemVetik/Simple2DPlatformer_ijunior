using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    [SerializeField] private CharacterMovement2D _movement;
    [SerializeField] private SpriteRenderer _sprite;
    private void Update()
    {
        Vector2 moveDir = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
        _movement.Move(moveDir);

        if (Input.GetKeyDown(KeyCode.Space))
            _movement.Jump();

        if (_movement.Velocity.x != 0)
        {
            _sprite.flipX = _movement.Velocity.x < 0 ? true : _movement.Velocity.x > 0 ? false : _sprite.flipX;
        }
    }
}


