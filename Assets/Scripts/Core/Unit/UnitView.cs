using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class UnitView : MonoBehaviour
{
    UnitEntity Entity;


    Rigidbody RigidbodyCached;
    Rigidbody Rigidbody {
        get {
            if (!RigidbodyCached)
            {
                RigidbodyCached = GetComponent<Rigidbody>();
            }
            return RigidbodyCached;
        }
    }

    public Vector3 Position => transform.position;

    public void SetEntity(UnitEntity entity)
    {
        Entity = entity;
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void SetVelocity(Vector3 velocity)
    {
        Rigidbody.velocity = velocity;
    }

    public void Stop()
    {
        Rigidbody.velocity = Vector3.zero;
    }
}
