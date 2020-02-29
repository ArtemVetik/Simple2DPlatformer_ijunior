using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMovement2D : MonoBehaviour
{
    [SerializeField] private Collider2D _collider;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] [Range(0f, 1f)] private float _groundNormal;

    private Rigidbody2D _body;

    public bool OnGround { get; private set; }
    public Vector2 Velocity => _body.velocity;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    public void Jump()
    {
        _body.velocity = new Vector2(_body.velocity.x, 0);
        _body.AddForce(Vector2.up * _jumpForce);
    }

    public void Move(Vector2 moveDirection)
    {
        moveDirection *= _moveSpeed;

        float minGroundNormalY = 1f;
        RaycastHit2D[] hits = new RaycastHit2D[16];
        int hitCount = _collider.Cast(moveDirection.normalized, hits, 0.1f);
        for (int i = 0; i < hitCount; i++)
        {
            if (hits[i].normal.y < minGroundNormalY)
                minGroundNormalY = hits[i].normal.y;
            if (hits[i].normal.y < _groundNormal && hits[i].normal.y > -_groundNormal)
            {
                _body.velocity = new Vector2(0, _body.velocity.y);
                return;
            }
        }

        float direction = moveDirection.x == 0 ? 0 : Mathf.Sign(moveDirection.x);
        _body.velocity = new Vector2(_moveSpeed * direction, _body.velocity.y * minGroundNormalY);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (var contact in collision.contacts)
        {
            if (contact.normal.y >= _groundNormal)
            {
                OnGround = true;
                break;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        OnCollisionEnter2D(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        OnGround = false;
    }
}
