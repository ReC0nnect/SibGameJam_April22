using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class UnitInput : MonoBehaviour
{
    public Animation_Script Anim;
    public GameObject Exit_Menu;
    public Time_Controller TimeContr;

 
    private bool isPaused = false;
    public bool isDead = false;


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
        Other_Buttons();
        Movement();
    }

    void Movement()
    {
        var direction = Vector3.zero;
        var isRight = false;

        if (isDead) 
        { 
            return; 
        }

        if (Input.GetKey(KeyCode.D))
        {
            direction.x += 1f;
            direction.z += 1f;
            isRight = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction.x += -1f;
            direction.z += -1f;
            isRight = false;
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

        if (Input.GetKey(KeyCode.Space))
        {
            Anim.AttackAnim();
        }

        if (direction != Vector3.zero)
        {
            UnitView.SetVelocity(direction.normalized * F.Settings.PlayerSpeed);
        }
        else
        {
            UnitView.Stop(); //TODSO 1
        }


   

        Anim.Flip(isRight);
        Anim.SetHorizontalMovement(direction.x, direction.z);
    }

    void Other_Buttons()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                Exit_Menu.SetActive(true);
                TimeContr.DoPause();
                isPaused = true;
                //return;
            }
            else
            {
                Exit_Menu.SetActive(false);
                TimeContr.UnPause();
                isPaused = false;
            }

        }
    }
}
