using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionEntity
{
    public static SessionEntity Current;

    public int LevelNumber { get; private set; }

    public PlayerEntity Player { get; private set; }
    public UnitEntity Follower { get; private set; }
    public EnemyController Enemy { get; private set; }
    public CubeController Cube { get; private set; }
    public PortalController Portal { get; private set; }

    public Audio_Manager SFX { get; private set; }

    public float PlayTime { get; private set; }

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
        PlayTime += Time.deltaTime;
    }

    void Init()
    {
        Player = new PlayerEntity(this);
        Enemy = new EnemyController(this);
        Cube = new CubeController(this);
        Portal = new PortalController(this);
        SFX = GameObject.FindObjectOfType<Audio_Manager>();
        Portal.CreatePortal();

        Player.OnFallingFinished += LoadNextLevel;
    }

    public void LoadNextLevel()
    {
        Cube = new CubeController(this);
        Portal.CreatePortal();

        if (LevelNumber == F.Settings.LevelWithFollower)
        {
            Vector3 position;
            do
            {
                position = Utilities.GeneratePosition(Player.BlockPosition, F.Settings.FolowerSpawnRange);

            } while (!Cube.IsEmptyPosition(position));
            Follower = new UnitEntity(this, position);
        }
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

    public void Win()
    {
        UI_Controller.Instance.ShowWinPanel(PlayTime);
    }
}
