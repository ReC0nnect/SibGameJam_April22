using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FIreCont : MonoBehaviour
{
    SessionEntity Session;

    UnitEntity Target;

    UnitView UnitCached;
    UnitView Unit
    {
        get
        {
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
    }

    void Update()
    {
        Unit.SetVelocity((Target.NormalizedPosition - Unit.Entity.NormalizedPosition).normalized * Unit.Description.Speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
