using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[CreateAssetMenu(fileName = "CommonSettings", menuName = "Create Common Settings")]
public class CommonSettings : ScriptableObject
{
    [Header("Player")]
    public float PlayerSpeed = 5f;
    
    [Header("Cube")]
    public Vector3 CubePlayerOffset = new Vector3(0f, -5f, 0f);
    public float CubeMiddlePointRadius = 5f;
    public float CubeRadius = 5f;
    public float CubeMovingSpeed = 5f;

    [Header("Enemies")]
    public float EnemySpawnRadius = 15f;
    public float EnemySpawnDelay = 15f;
    public float EnemyMovingSpeed = 3f;
}
