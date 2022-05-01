using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryCube : MonoBehaviour
{
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
        transform.localPosition = position;
    }
}
