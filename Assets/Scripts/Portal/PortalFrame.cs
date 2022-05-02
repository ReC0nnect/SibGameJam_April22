using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalFrame : MonoBehaviour
{
    PortalEntity Entity;

    public Vector3 Position => transform.localPosition;
    public int Index { get; private set; }

    public void Init(PortalEntity entity, Vector3 position, int index)
    {
        Entity = entity;
        transform.localPosition = position;
        SetIndex(index);
    }

    public void SetIndex(int index)
    {
        Index = index;
    }
}
