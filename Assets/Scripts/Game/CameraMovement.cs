using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] private float _duration;
    [SerializeField] private float offcetX;

    private float xPos;

    private void Start()
    {
        transform.position = new Vector3(_target.position.x + offcetX, transform.position.y, transform.position.z);
    }
    private void LateUpdate()
    {
        if (_target == null)
            return;

        xPos = Mathf.Lerp(transform.position.x, _target.position.x + offcetX, _duration * Time.deltaTime);
        transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
    }
}
