using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitEntity
{
    public UnitView View { get; protected set; }
    public SessionEntity Session { get; }
    public Vector3 Position => View.Position;
    public Vector3 NormalizedPosition => new Vector3(Position.x, 0f, Position.z);
    public Vector3 BlockPosition => new Vector3(Mathf.Round(Position.x), 0f, Mathf.Round(Position.z));

    public bool IsAlive { get; private set; } = true;

    public event Action<UnitEntity> OnDeath;

    public UnitEntity(SessionEntity session)
    {
        Session = session;
    }

    public UnitEntity(SessionEntity session, Vector3 position, int Random_Num)
    {
        Session = session;

        if (Random_Num == 0) { View = GameObject.Instantiate(F.Prefabs.Enemy_1); } // пока так оставлю для случайного врага
        else if (Random_Num == 1) { View = GameObject.Instantiate(F.Prefabs.Enemy_2); }
        else if (Random_Num == 2) { View = GameObject.Instantiate(F.Prefabs.Enemy_3); }
        else if (Random_Num == 3) { View = GameObject.Instantiate(F.Prefabs.Enemy_4); }
        else if (Random_Num == 4) { View = GameObject.Instantiate(F.Prefabs.Enemy_5); }
        else if (Random_Num == 5) { View = GameObject.Instantiate(F.Prefabs.Enemy_6); }

        View.SetPosition(position);
        View.SetEntity(this);

        var ai = View.gameObject.AddComponent<EnemyAI>();
        ai.Init(session);
    }

    public UnitEntity(SessionEntity session, Vector3 position)
    {
        Session = session;

        View = GameObject.Instantiate(F.Prefabs.Follower);
        View.SetPosition(position + Vector3.up * 1.5f + Vector3.down * session.LevelNumber * F.Settings.LevelDistance);
        View.SetEntity(this);


        var cube = session.Cube.AddFreeCube(position);

        var ai = View.gameObject.AddComponent<FollowAI>();
        ai.Init(session, cube);
    }

    public void SetPosition(Vector3 position)
    {
        View.SetPosition(position);
    }

    public void Kill()
    {
        OnDeath?.Invoke(this);
        GameObject.Destroy(View.gameObject);
        IsAlive = false;
    }
}
