using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitView : MonoBehaviour
{
    SessionEntity Session;

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

    public void Init(SessionEntity session)
    {
        Session = session;
    }

    public void SetVelocity(Vector3 velocity)
    {
        Rigidbody.velocity = velocity;
    }
}
