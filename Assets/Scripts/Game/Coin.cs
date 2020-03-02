using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Coin : MonoBehaviour
{
    [SerializeField] private int _cost;
    [SerializeField] private ParticleSystem _particles;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out CoinCollector collector))
        {
            collector.AddCoins(_cost);
            Instantiate(_particles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
