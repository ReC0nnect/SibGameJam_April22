using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionEntity
{
    public static SessionEntity Current;

    public UnitView Player;
    public EnemyController EnemyController;

    public static void Create()
    {
        Current = new SessionEntity();
        Current.Init();
    }

    void Init()
    {
        InitPlayer();
        EnemyController = new EnemyController(this);
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
