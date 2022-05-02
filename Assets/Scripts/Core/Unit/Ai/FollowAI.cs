using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAI : MonoBehaviour
{
    SpriteRenderer View;
    SessionEntity Session;

    UnitEntity Target;

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
            var direction = Target.NormalizedPosition - Unit.Entity.NormalizedPosition;
            if (direction.sqrMagnitude > 2f)
            {
                Unit.SetVelocity(direction.normalized * F.Settings.FollowMovingSpeed);

                View.flipX = Target.Position.x > Unit.transform.position.x;
            }
        }
    }
}
