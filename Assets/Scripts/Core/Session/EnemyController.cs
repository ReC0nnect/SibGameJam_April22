using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController
{
    float SpawnTime;

    public List<UnitEntity> Enemies { get; private set; }

    SessionEntity Session { get; }

    public EnemyController(SessionEntity session)
    {
        Session = session;
        SpawnTime = Time.timeSinceLevelLoad + F.Settings.EnemySpawnDelay;
        Enemies = new List<UnitEntity>();
    }

    public void Update()
    {
        if (SpawnTime < Time.timeSinceLevelLoad)
        {
            SpawnTime = Time.timeSinceLevelLoad + F.Settings.EnemySpawnDelay;
            Spawn();
        }
    }

    public void Spawn()
    {
        var randX = UnityEngine.Random.value - 0.5f;
        var randZ = UnityEngine.Random.value - 0.5f;
        var direction = new Vector3(randX, 0f, randZ).normalized;
        var enemyPos = Session.PlayerEntity.Position + direction * F.Settings.EnemySpawnRadius;

        var enemyEntity = new UnitEntity(Session, enemyPos);
        Enemies.Add(enemyEntity);
    }
}
