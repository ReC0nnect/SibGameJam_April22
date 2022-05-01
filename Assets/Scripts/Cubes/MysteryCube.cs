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
        var playerBottomPoint = Entity.Session.Player.Position + F.Settings.CubePlayerOffset;
        var middle1Point = playerBottomPoint;
        middle1Point.x += UnityEngine.Random.Range(-F.Settings.CubeMiddlePointRadius, F.Settings.CubeMiddlePointRadius);
        middle1Point.z += UnityEngine.Random.Range(-F.Settings.CubeMiddlePointRadius, F.Settings.CubeMiddlePointRadius);
        var middle2Point = playerBottomPoint;
        middle2Point.x += UnityEngine.Random.Range(-F.Settings.CubeMiddlePointRadius, F.Settings.CubeMiddlePointRadius);
        middle2Point.z += UnityEngine.Random.Range(-F.Settings.CubeMiddlePointRadius, F.Settings.CubeMiddlePointRadius);

        Collider.isTrigger = true;
        for (float t = 0f; t < 1; t += Time.deltaTime * F.Settings.CubeMovingSpeed)
        {
            transform.localPosition = GetCubicBezierPoint(startPosition, middle1Point, middle2Point, finishPosition, t);
            yield return null;
        }
        Collider.isTrigger = false;
        transform.localPosition = finishPosition;
    }

    Vector3 GetQuadraticBezierPoint (Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        var p0p1 = (1 - t) * p0 + t * p1;
        var p1p2 = (1 - t) * p1 + t * p2;
        return (1 - t) * p0p1 + t * p1p2;
    }

    Vector3 GetCubicBezierPoint (Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        var p0p1 = (1 - t) * p0 + t * p1;
        var p1p2 = (1 - t) * p1 + t * p2;
        var p2p3 = (1 - t) * p2 + t * p3;

        var p0p1_p1p2 = (1 - t) * p0p1 + t * p1p2;
        var p1p2_p2p3 = (1 - t) * p1p2 + t * p2p3;

        return (1 - t) * p0p1_p1p2 + t * p1p2_p2p3;
    }
}
