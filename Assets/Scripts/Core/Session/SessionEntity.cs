using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionEntity
{
    public static SessionEntity Current;

    public UnitEntity Player { get; private set; }
    public EnemyController Enemy { get; private set; }
    public CubeController Cube { get; private set; }

    public static SessionEntity Create()
    {
        Current = new SessionEntity();
        Current.Init();
        return Current;
    }

    public void Update()
    {
        Cube.Update();
        Enemy.Update();
    }

    void Init()
    {
        Player = new UnitEntity(this);
        Enemy = new EnemyController(this);
        Cube = new CubeController(this);
    }
}
