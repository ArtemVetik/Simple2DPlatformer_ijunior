using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Класс "контроллера движения" любым персонажем
[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMovement2DGleb : MonoBehaviour
{
    public bool OnGround => _groundColliders.Count > 0;
    public Vector2 Velocity => _body.velocity;

    [SerializeField] private Collider2D _collider;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;

    private Rigidbody2D _body;
    private Vector2 _moveDirection;
    private List<Collider2D> _groundColliders;
    private float ANGLEKOEF = 0.65f;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _groundColliders = new List<Collider2D>();
    }

    public void Jump()
    {
        _body.AddForce(transform.up * _jumpForce);
    }

    public void Movement(Vector2 moveDirection)
    {
        moveDirection *= _moveSpeed;

        if (moveDirection.x == 0)
        {
            _body.velocity = new Vector2(0, _body.velocity.y);
            return;
        }

        RaycastHit2D[] hits = new RaycastHit2D[16];
        float yVelocityKf = 1f; // Коэффициент притяжения на склонах (чтоб не подпрыгивал персонаж сильно высоко при подъёме вверх)
        int hitCount = _collider.Cast(moveDirection.normalized, hits, 0.1f);
        for (int i = 0; i < hitCount; i++)
        {
            if (hits[i].normal.y < yVelocityKf)
                yVelocityKf = hits[i].normal.y;
            if (hits[i].normal.y < ANGLEKOEF)
            {
                _body.velocity = new Vector2(0, _body.velocity.y); 
                return;
            }
        }

        _body.velocity = new Vector2(_moveSpeed * Mathf.Sign(moveDirection.x), _body.velocity.y * yVelocityKf);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (_groundColliders.Contains(collision.collider))
            _groundColliders.Remove(collision.collider);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (_groundColliders.Contains(collision.collider) == false)
        {
            foreach (var contact in collision.contacts)
            {
                if (contact.point.y < _collider.bounds.min.y)
                {
                    _groundColliders.Add(collision.collider);
                    break;
                }
            }
        }
    }
}
