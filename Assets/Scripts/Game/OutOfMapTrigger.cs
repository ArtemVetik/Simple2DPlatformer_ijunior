using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfMapTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Unit unit))
        {
            unit.TakeDamage(int.MaxValue);
        }
    }
}
