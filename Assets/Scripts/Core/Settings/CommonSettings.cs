using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[CreateAssetMenu(fileName = "CommonSettings", menuName = "Create Common Settings")]
public class CommonSettings : ScriptableObject
{
    [Header("Player")]
    public float PlayerSpeed = 5f;
    public float FollowMovingSpeed = 10f;
    public Vector2 FolowerSpawnRange = new Vector2(8f, 14f);
    public int LevelWithFollower = 1;

    [Header("Cube")]
    public Vector3 CubePlayerOffset = new Vector3(0f, -5f, 0f);
    public Vector3 CubePlayerAttackOffset = new Vector3(0f, 5f, 0f);
    public float CubeAttackRotation = 960f;
    public float CubeMiddlePointRadius = 5f;
    public float CubeRadius = 5f;
    public float CubeMovingSpeed = 5f;
    public float CubeFallingSpeed = 2f;
    public float CubeAttackTime = 5f;

    [Header("Background Cube")]
    public Vector2 BackgroundCubeDepth = new Vector2(5f, 16f);
    public Vector2 BackgroundCubeDistance = new Vector2(32f, 64f);
    public Vector2 BackgroundCubeSize = new Vector2(1.5f, 5f);
    [Range(0f, 1f)] public float BackgroundCubeScaleChance = 0.7f;
    public int BackgroundCubeCount = 32;

    [Header("Free Cube")]
    public Vector2 FreeCubeRange = new Vector2();
    public int FreeCubeCount = 128;
    public float FreeCubeMaxDistance = 32f;

    [Header("Enemies")]
    public float EnemySpawnRadius = 15f;
    public float EnemySpawnDelay = 15f;
    public float EnemyMovingSpeed = 3f;

    [Header("Portal")]
    public Vector2 PortalSpawnRange = new Vector2(32f, 64f);
    public Vector2 PortalFrameSpawnRange = new Vector2(24f, 48f);
    public Vector2 PortalFrameStartCount = new Vector2(3f, 8f);
    public float PortalFrameForbiddenDistanceToPortal = 8f;
    public float PortalTipDelay = 30f;
    public int PortalFrameSpawnExtraCount = 8;
    
    [Header("Portal Down")]
    public int PortalFrameCount = 12;
    public int PortalEntranceCount = 4;

    [Header("Portal Up")]
    public int PortalUpFrameCount = 12;
    public int PortalUpEntranceCount = 4;

    [Header("Portal Vortex")]
    public float PortalVortexAttractRange = 3f;
    public float PortalVortexAttractForce = 5f;

    [Header("Falling")]
    public float FallingTime = 7f;
    public float FlightUpTime = 10f;
    public float LevelDistance = 100f;
}
