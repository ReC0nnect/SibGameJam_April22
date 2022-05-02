using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    public static Vector3 GeneratePosition(Vector3 playerPos, Vector2 range)
    {
        var direction = new Vector3()
        {
            x = Random.value - 0.5f,
            y = 0f,
            z = Random.value - 0.5f
        };
        var position = playerPos + direction.normalized * Random.Range(range.x, range.y);
        position.x = Mathf.Round(position.x);
        position.y = 0f;
        position.z = Mathf.Round(position.z);
        return position;
    }



    public static Vector3 GetQuadraticBezierPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        var p0p1 = (1 - t) * p0 + t * p1;
        var p1p2 = (1 - t) * p1 + t * p2;
        return (1 - t) * p0p1 + t * p1p2;
    }

    public static Vector3 GetCubicBezierPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        var p0p1 = (1 - t) * p0 + t * p1;
        var p1p2 = (1 - t) * p1 + t * p2;
        var p2p3 = (1 - t) * p2 + t * p3;

        var p0p1_p1p2 = (1 - t) * p0p1 + t * p1p2;
        var p1p2_p2p3 = (1 - t) * p1p2 + t * p2p3;

        return (1 - t) * p0p1_p1p2 + t * p1p2_p2p3;
    }
}
