using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController
{
    SessionEntity Session { get; }

    public EnemyController(SessionEntity session)
    {
        Session = session;
    }

    public void Spawn()
    {
        var position = Session.Player.transform.position;
        var randX = UnityEngine.Random.value - 0.5f;
        var randZ = UnityEngine.Random.value - 0.5f;
        var direction = new Vector3(randX, 0f, randZ).normalized;
        var enemyPos = position + direction * F.Settings.EnemySpawnRadius;

        var enemyGo = GameObject.Instantiate(F.Prefabs.Enemy);
        enemyGo.transform.position = enemyPos;
    }
}
