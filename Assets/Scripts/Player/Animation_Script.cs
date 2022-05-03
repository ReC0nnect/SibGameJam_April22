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

    public void AttackAnim()
    {
        anim.SetTrigger("Attack");
    }
    public void SetHorizontalMovement(float x, float y)
    {
        anim.SetFloat("HorizontalAxis", x);

        anim.SetFloat("VerticalAxis", y);

        anim.SetFloat("Speed", RB.velocity.sqrMagnitude);
        //anim.SetFloat("HorVelocity", xVel);
        //anim.SetFloat("VerticalVelocity", yVel);
    }

    public void KillPlayer()
    {
        anim.SetBool("Dead", true);
    }

    public void StartFalling()
    {
        anim.SetBool("Fall", true);
    }

    public void StopFalling()
    {
        anim.SetBool("Fall", false);
    }

    public void WalkSFX()
    {
        SessionEntity.Current.SFX.Play("Walking");
    }

    public void Flip(bool state)
    {
        Player_Sprite.flipX = state;
    }
}
