using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[CreateAssetMenu(fileName = "CommonSettings", menuName = "Create Common Settings")]
public class CommonSettings : ScriptableObject
{
    public float EnemySpawnRadius = 15f;
    public float PlayerSpeed = 5f;
    public int CubesCount = 32;
    public float CubeRadius = 5f;
    public float CubeMovingSpeed = 5f;
}
