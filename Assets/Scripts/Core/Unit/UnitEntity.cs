using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitEntity
{
    public UnitView View { get; }
    public SessionEntity Session { get; }
    public Vector3 Position => View.Position;

    public UnitEntity(SessionEntity session)
    {
        Session = session;

        var playerGo = GameObject.FindGameObjectWithTag("Player");
        if (playerGo)
        {
            View = playerGo.GetComponent<UnitView>();
            View.SetEntity(this);
        }
    }

    public UnitEntity(SessionEntity session, Vector3 position)
    {
        Session = session;

        View = GameObject.Instantiate(F.Prefabs.Enemy);
        View.SetPosition(position);

        var ai = View.gameObject.AddComponent<EnemyAI>();
        ai.Init(session);
    }
}
