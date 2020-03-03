using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTrigger : MonoBehaviour
{
    [SerializeField] private float _hitDelay = 1f;

    private Dictionary<Unit, bool> _unitsInWater;
    private WaitForSeconds _pause;

    private void Start()
    {
        _unitsInWater = new Dictionary<Unit, bool>();
        _pause = new WaitForSeconds(_hitDelay);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Unit unit))
        {
            _unitsInWater.Add(unit, true);
            StartCoroutine(Hit(unit, _hitDelay));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Unit unit))
        {
            if (_unitsInWater.ContainsKey(unit))
                _unitsInWater[unit] = false;
        }
    }

    IEnumerator Hit(Unit unit, float delay)
    {
        while (_unitsInWater[unit])
        {
            unit.TakeDamage(1);
            yield return _pause;
        }
        _unitsInWater.Remove(unit);
    }
}
