using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class UnitInput : MonoBehaviour
{
    [SerializeField] float Speed = 5f;

    UnitView UnitViewCached;
    UnitView UnitView {
        get {
            if (!UnitViewCached)
            {
                UnitViewCached = GetComponent<UnitView>();
            }
            return UnitViewCached;
        }
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        var direction = Vector3.zero;
        if (Input.GetKey(KeyCode.D))
        {
            direction.x = 1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction.x = -1f;
        }
        if (Input.GetKey(KeyCode.W))
        {
            direction.z = 1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction.z = -1f;
        }

        if (direction != Vector3.zero)
        {
            UnitView.SetVelocity(direction.normalized * Speed);
        }
    }
}
