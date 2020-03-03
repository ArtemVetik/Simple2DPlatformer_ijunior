using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _duration;
    [SerializeField] private float _offcetX;

    private float _posX;

    private void Start()
    {
        transform.position = new Vector3(_target.position.x + _offcetX, transform.position.y, transform.position.z);
    }

    private void LateUpdate()
    {
        if (_target == null)
            return;

        _posX = Mathf.Lerp(transform.position.x, _target.position.x + _offcetX, _duration * Time.deltaTime);
        transform.position = new Vector3(_posX, transform.position.y, transform.position.z);
    }
}
