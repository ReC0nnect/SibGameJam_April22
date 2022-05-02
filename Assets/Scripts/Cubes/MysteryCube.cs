using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryCube : MonoBehaviour
{
    [SerializeField] Collider Collider;
    [SerializeField] Material DefaultMaterial;
    [SerializeField] Material FreeMaterial;
    [SerializeField] MeshRenderer Mesh;
    [SerializeField] public bool IsPortalFrame;

    MysteryCubeEntity Entity;
    Vector3? LastTargetPosition;

    public Vector3 NormalizedPosition => transform.localPosition;
    public Vector3 Position => transform.position;

    public void Init(MysteryCubeEntity entity)
    {
        Entity = entity;
    }

    public void SetPosition(Vector3 position)
    {
        if (MovingCoroutine != null)
        {
            StopCoroutine(MovingCoroutine);
        }
        MovingCoroutine = StartCoroutine(BezierMoving(position));
    }

    Coroutine MovingCoroutine;
    IEnumerator BezierMoving(Vector3 finishPosition)
    {
        var startPosition = NormalizedPosition;
        var playerBottomPoint = Entity.Session.Player.NormalizedPosition + F.Settings.CubePlayerOffset;
        var middle1Point = playerBottomPoint;
        middle1Point.x += UnityEngine.Random.Range(-F.Settings.CubeMiddlePointRadius, F.Settings.CubeMiddlePointRadius);
        middle1Point.z += UnityEngine.Random.Range(-F.Settings.CubeMiddlePointRadius, F.Settings.CubeMiddlePointRadius);
        var middle2Point = playerBottomPoint;
        middle2Point.x += UnityEngine.Random.Range(-F.Settings.CubeMiddlePointRadius, F.Settings.CubeMiddlePointRadius);
        middle2Point.z += UnityEngine.Random.Range(-F.Settings.CubeMiddlePointRadius, F.Settings.CubeMiddlePointRadius);

        var randValue = Mathf.Round(UnityEngine.Random.value * 1000f);
        var rotationAngle = new Vector3
        {
            x = randValue % 2f == 0f ? F.Settings.CubeAttackRotation : 0f,
            y = F.Settings.CubeAttackRotation,
            z = randValue % 3f == 0f ? F.Settings.CubeAttackRotation : 0f
        };

        Collider.isTrigger = true;
        for (float t = 0f; t < 1; t += Time.deltaTime * F.Settings.CubeMovingSpeed)
        {
            transform.localPosition = Utilities.GetCubicBezierPoint(startPosition, middle1Point, middle2Point, finishPosition, t);
            transform.rotation = Quaternion.Euler(rotationAngle * t);
            yield return null;
        }
        Collider.isTrigger = false;
        transform.localPosition = finishPosition;
        transform.rotation = Quaternion.identity;
        MovingCoroutine = null;
    }

    public void ChangeMaterial(bool state)
    {
        if (IsPortalFrame) { return; }

        Mesh.material = state ? DefaultMaterial : FreeMaterial;
    }

    public void Shoot(UnitEntity target)
    {
        if (MovingCoroutine != null)
        {
            StopCoroutine(MovingCoroutine);
            MovingCoroutine = null;
        }
        StartCoroutine(Shooting(target));
    }

    IEnumerator Shooting(UnitEntity target)
    {
        var startPosition = NormalizedPosition; 
        var middlePoint = Entity.Session.Player.NormalizedPosition + F.Settings.CubePlayerAttackOffset;
        middlePoint.x += UnityEngine.Random.Range(-F.Settings.CubeMiddlePointRadius, F.Settings.CubeMiddlePointRadius);
        middlePoint.z += UnityEngine.Random.Range(-F.Settings.CubeMiddlePointRadius, F.Settings.CubeMiddlePointRadius);

        var randValue = Mathf.Round(UnityEngine.Random.value * 1000f);
        var rotationAngle = new Vector3
        {
            x = randValue % 2f == 0f ? F.Settings.CubeAttackRotation : 0f,
            y = randValue % 3f == 0f ? F.Settings.CubeAttackRotation : 0f,
            z = F.Settings.CubeAttackRotation
        };

        target.OnDeath += OnTargetDeath;

        Collider.isTrigger = true;
        for (float t = 0f; t < 1; t += Time.deltaTime / F.Settings.CubeAttackTime)
        {
            var targetPosition = LastTargetPosition ?? target.NormalizedPosition;
            transform.localPosition = Utilities.GetQuadraticBezierPoint(startPosition, middlePoint, targetPosition, t);
            transform.rotation = Quaternion.Euler(rotationAngle * t);
            yield return null;
        }
        if (target.IsAlive)
        {
            target.Kill();
        }
        Destroy(gameObject);
    }

    void OnTargetDeath(UnitEntity target)
    {
        target.OnDeath -= OnTargetDeath;
        LastTargetPosition = target.NormalizedPosition;
    }
}
