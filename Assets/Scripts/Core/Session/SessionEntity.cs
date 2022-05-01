using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionEntity
{
    public static SessionEntity Current;

    public UnitView Player;
    public EnemyController EnemyController;
    public CubeController Cube;

    public static void Create()
    {
        Current = new SessionEntity();
        Current.Init();
    }

    public static void Update()
    {
        Current.Cube.Update();
    }

    void Init()
    {
        InitPlayer();
        EnemyController = new EnemyController(this);
        Cube = new CubeController(this);
    }

    void InitPlayer()
    {
        var playerGo = GameObject.FindGameObjectWithTag("Player");
        if (playerGo)
        {
            Player = playerGo.GetComponent<UnitView>();
            Player.Init(this);
        }
    }
}
