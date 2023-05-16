using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameFieldBoundBehaviour : MonoBehaviour
{
    public BoxCollider2D col;
    public Rigidbody2D rb;
    public CameraProps cam;
    private const float Tolerance = .1f;

    private void OnTriggerExit2D(Collider2D charCollider)
    {
        var toTranslate = GameObject.FindGameObjectsWithTag("Enemy")
            .Concat(GameObject.FindGameObjectsWithTag("Projectile"))
            .Where(obj => cam.InBounds(obj.transform.position))
            .ToArray();
        var colPt = col.ClosestPoint(charCollider.transform.position);
        var pt = colPt;
        if (Math.Abs(col.bounds.min.x - pt.x) < Tolerance)
            pt.x = col.bounds.max.x;
        else if (Mathf.Abs(col.bounds.max.x - pt.x) < Tolerance)
            pt.x = col.bounds.min.x;
        else if (Mathf.Abs(col.bounds.min.y - pt.y) < Tolerance)
            pt.y = col.bounds.max.y;
        else if (Mathf.Abs(col.bounds.max.y - pt.y) < Tolerance)
            pt.y = col.bounds.min.y;
        charCollider.transform.position = pt;
        var delta = pt - colPt;
        foreach (var obj in toTranslate)
            obj.transform.position += (Vector3)delta;
    }
}