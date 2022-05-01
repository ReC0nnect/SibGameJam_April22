using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class UnitInput : MonoBehaviour
{
    public Animation_Script Anim;
    private int side;

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
            direction.x += 1f;
            direction.z += 1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction.x += -1f;
            direction.z += -1f;
        }
        if (Input.GetKey(KeyCode.W))
        {
            direction.x += -1f;
            direction.z += 1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction.x += 1f;
            direction.z += -1f;
        }

        if (direction != Vector3.zero)
        {
            UnitView.SetVelocity(direction.normalized * F.Settings.PlayerSpeed);
        }

        if (direction.x < 0 || (direction.x < 0 && direction.z < 0))
        {
            side = 1;
            Anim.Flip(side);
        }
        else if (direction.x > 0 || (direction.x > 0 && direction.z > 0))
        {
            side = -1;
            Anim.Flip(side);
        }


        Anim.SetHorizontalMovement(direction.x, direction.z);

    }
}
