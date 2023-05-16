using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform playerTransform;

    void FixedUpdate()
    {
        transform.position = playerTransform.position;
    }
}
