using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Script : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    public SpriteRenderer Player_Sprite;

    public Rigidbody RB;
    private void Awake()
    {
        Player_Sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        
        
    }
    public void SetHorizontalMovement(float x, float y)
    {
        anim.SetFloat("HorizontalAxis", x);

        anim.SetFloat("VerticalAxis", y);

        anim.SetFloat("Speed", RB.velocity.sqrMagnitude);
        //anim.SetFloat("HorVelocity", xVel);
        //anim.SetFloat("VerticalVelocity", yVel);
    }

    public void Flip(int side)
    {

        /*  if (move.wallGrab || move.wallSlide)
          {
              if (side == -1 && sr.flipX)
                  return;

              if (side == 1 && !sr.flipX)
              {
                  return;
              }
          }
          */
        bool state = (side == 1) ? false : true;
        Player_Sprite.flipX = state;
    }
}
