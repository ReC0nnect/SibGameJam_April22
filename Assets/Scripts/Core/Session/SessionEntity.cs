using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionEntity
{
    public static SessionEntity Current;

    public int LevelNumber { get; private set; }

    public PlayerEntity Player { get; private set; }
    public EnemyController Enemy { get; private set; }
    public CubeController Cube { get; private set; }
    public PortalController Portal { get; private set; }

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
        Player = new PlayerEntity(this);
        Enemy = new EnemyController(this);
        Cube = new CubeController(this);
        Portal = new PortalController(this);
        Portal.CreatePortal();

        Player.OnFallingFinished += LoadNextLevel;
    }

    public void LoadNextLevel()
    {
        Cube = new CubeController(this);
        Portal.CreatePortal();
    }

    public void GoNextLevel()
    {
        LevelNumber++;
        Player.StartFalling();
    }

    public void Clear()
    {
        Enemy.Clear();
        Cube.Clear();
        Portal.Clear();
    }
}
