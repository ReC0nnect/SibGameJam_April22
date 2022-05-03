using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAI : MonoBehaviour
{
    SpriteRenderer View;
    SessionEntity Session;

    PlayerEntity Target;

    public bool Activated { get; private set; }

    UnitView UnitCached;
    UnitView Unit {
        get {
            if (!UnitCached)
            {
                UnitCached = GetComponent<UnitView>();
            }
            return UnitCached;
        }
    }

    public void Init(SessionEntity session, MysteryCubeEntity cube)
    {
        Session = session;

        Target = session.Player;
        View = GetComponentInChildren<SpriteRenderer>();

        cube.OnCapture += Activate;
    }

    void Activate(MysteryCubeEntity cube)
    {
        cube.OnCapture -= Activate;
        Activated = true;
    }

    void Update()
    {
        if (Activated)
        {
            Vector3 direction;
            float speed;
            if (Target.IsFalling)
            {
                direction = Target.Position - Unit.Entity.Position;
                speed = F.Settings.LevelDistance / F.Settings.FallingTime;
            }
            else
            {
                direction = Target.NormalizedPosition - Unit.Entity.NormalizedPosition;
                speed = F.Settings.FollowMovingSpeed;
            }
            if (direction.sqrMagnitude > 2f)
            {
                var velocity = direction.normalized * speed;
                Unit.SetVelocity(velocity);
                //View.flipX = Target.Position.x > Unit.transform.position.x;

                View.flipX = (velocity.x > 0f && velocity.z > 0f
                || velocity.normalized.x > -0.5f && velocity.z > 0f
                || velocity.x > 0f && velocity.normalized.z > -0.5f);

                Unit.AnimScript.SetHorizontalMovement(direction.x, direction.z);
            }
        }
    }
}
