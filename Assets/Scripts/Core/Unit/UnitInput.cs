using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class UnitInput : MonoBehaviour
{
    [SerializeField] float Speed = 5f;

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
            Rigidbody.velocity = direction.normalized * Speed;
        }
    }
}
