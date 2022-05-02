using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    SessionEntity Session;

    UnitEntity Target;
    public SpriteRenderer Unit_Sprite;

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

    public void Init(SessionEntity session)
    {
        Session = session;

        Target = session.Player;

        Unit_Sprite = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        Unit.SetVelocity((Target.NormalizedPosition - Unit.Entity.NormalizedPosition).normalized * F.Settings.EnemyMovingSpeed);
        if (Target.Position.x > Unit.transform.position.x)
        {
            Unit_Sprite.flipX = true;
        }
        else
        {
            Unit_Sprite.flipX = false;
        }

    }
}
