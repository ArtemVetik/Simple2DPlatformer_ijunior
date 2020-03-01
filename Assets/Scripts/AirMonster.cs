﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AirMonster : Enemy
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _flyDistance;

    private Rigidbody2D _body;
    private Vector2 _startPosition;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _startPosition = transform.position;
        Direction = MoveDirection.Right;
    }
    private void Update()
    {
        _body.velocity = new Vector2(_speed * (int)Direction, 0);

        if (((Vector2)transform.position - _startPosition).magnitude > _flyDistance)
        {
            Direction = Direction.Flip();
            _startPosition = transform.position;
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        Direction = (MoveDirection)(int)Mathf.Sign(transform.position.x - collision.contacts[0].point.x);
        _startPosition = transform.position;
        base.OnCollisionEnter2D(collision);
    }

    protected override void Dead()
    {
        _animator.SetBool("hit", true);
        _speed = 0;
        _collider.enabled = false;
    }
}
