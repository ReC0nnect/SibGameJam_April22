using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyController
{
    float SpawnTime;

    SessionEntity Session { get; }

    public List<UnitEntity> Enemies { get; private set; }
    public bool HasEnemy => Enemies.Count > 0;


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
        var enemyPos = Session.Player.Position + direction * F.Settings.EnemySpawnRadius;

        var enemyEntity = new UnitEntity(Session, enemyPos);
        Enemies.Add(enemyEntity);
        enemyEntity.OnDeath += EnemyDeath;
    }

    void EnemyDeath(UnitEntity enemy)
    {
        enemy.OnDeath -= EnemyDeath;
        Enemies.Remove(enemy);
    }

    public UnitEntity GetNearestOfPlayer()
    {
        if (HasEnemy)
        {
            return Enemies.OrderBy(e => (e.Position - Session.Player.Position).sqrMagnitude).First();
        }
        return null;
    }
}
