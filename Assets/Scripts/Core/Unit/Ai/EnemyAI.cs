using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    SessionEntity Session;

    UnitEntity Target;


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

        Target = session.PlayerEntity;
    }

    void Update()
    {
        Unit.SetVelocity((Target.Position - Unit.Position).normalized * F.Settings.EnemyMovingSpeed);
    }
}
