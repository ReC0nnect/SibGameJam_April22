using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Vector3 Offset;

    Transform TargetCached;
    Transform Target {
        get {
            if (!TargetCached)
            {
                var unit = FindObjectOfType<UnitInput>();
                if (unit)
                {
                    TargetCached = unit.transform;
                }
            }
            return TargetCached;
        }
    }

    void Awake()
    {
        
    }

    void LateUpdate()
    {
        transform.position = Target.position + Offset;
    }
}
