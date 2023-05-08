using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraProps : MonoBehaviour
{
    private Camera _cam;
    private void Awake()
    {
        _cam = Camera.main;
    }

    public float Right =>  gameObject.transform.position.x + _cam!.aspect * _cam.orthographicSize;

    public float Left => gameObject.transform.position.x - _cam!.aspect * _cam.orthographicSize;

    public float Top => gameObject.transform.position.y + _cam!.orthographicSize;

    public float Bottom => gameObject.transform.position.y - _cam!.orthographicSize;
}
