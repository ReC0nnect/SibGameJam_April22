using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryCube : MonoBehaviour
{
    [SerializeField] Collider Collider;

    MysteryCubeEntity Entity;

    public Vector3 Position => transform.localPosition;

    public void Init(MysteryCubeEntity entity)
    {
        Entity = entity;
    }

    void Update()
    {
        
    }

    public void SetPosition(Vector3 position)
    {
        StartCoroutine(BezierMoving(position));
    }

    IEnumerator BezierMoving(Vector3 finishPosition)
    {
        var startPosition = Position;
        var middlePosition = Entity.Session.Player.Position + Vector3.down * 5f;

        Collider.isTrigger = true;
        for (float t = 0f; t < 1; t += Time.deltaTime * F.Settings.CubeMovingSpeed)
        {
            transform.localPosition = GetBezierPoint(startPosition, middlePosition, finishPosition, t);
            yield return null;
        }
        Collider.isTrigger = false;
        transform.localPosition = finishPosition;
    }

    Vector3 GetBezierPoint (Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        var p0p1 = (1 - t) * p0 + t * p1;
        var p1p2 = (1 - t) * p1 + t * p2;
        return (1 - t) * p0p1 + t * p1p2;
    }
}
