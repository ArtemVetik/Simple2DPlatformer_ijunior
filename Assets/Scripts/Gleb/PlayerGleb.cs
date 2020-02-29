using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Класс персонажа
[RequireComponent(typeof(Animator))]
public class PlayerGleb : MonoBehaviour
{
    [SerializeField] private CharacterMovement2DGleb _characterMovement2D;
    [SerializeField] private SpriteRenderer _sprite;

    private Animator _animator;
    private Vector2 _moveDirection;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), 0);

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && _characterMovement2D.OnGround)
            _characterMovement2D.Jump();

        _animator.SetBool("Jump", _characterMovement2D.OnGround == false);
        _animator.SetBool("Run", _characterMovement2D.Velocity.x != 0);

        // if (_characterMovement2D.Velocity.x != 0)
        // {
        //   _sprite.flipX = _characterMovement2D.Velocity.x < 0;
        //}
        _sprite.flipX = _moveDirection.x < 0 ? true : _moveDirection.x > 0 ? false : _sprite.flipX;
    }

    private void FixedUpdate()
    {
        _characterMovement2D.Movement(_moveDirection);
    }
}
