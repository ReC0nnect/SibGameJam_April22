using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundCubes : MonoBehaviour
{
    [SerializeField] GameObject CubePrefab;

    float SqrMaxDistance;
    SessionEntity Session;
    List<GameObject> BackCubes = new List<GameObject>();

    public void Init(SessionEntity session) 
    {
        Session = session;
        SqrMaxDistance = F.Settings.BackgroundCubeDistance.y * F.Settings.BackgroundCubeDistance.y;
        SpawnAllCubes(Session.Player.Position);
    }

    public void Respawn(Vector3 playerPosition)
    {
        Clear();
        SpawnAllCubes(playerPosition);
    }

    void Update()
    {
        SpawnCube();
    }

    void SpawnAllCubes(Vector3 playerPosition)
    {
        for (int i = 0; i < F.Settings.BackgroundCubeCount; i++)
        {
            CreateBackCube(new Vector2(0f, F.Settings.BackgroundCubeDistance.y), playerPosition);
        }
    }

    void SpawnCube()
    {
        var playerPosition = Session.Player.Position;
        for (int i = 0; i < BackCubes.Count; i++)
        {
            if ((BackCubes[i].transform.position - playerPosition).sqrMagnitude > SqrMaxDistance)
            {
                CreateBackCube(F.Settings.BackgroundCubeDistance, Session.Player.Position);
                Destroy(BackCubes[i]);
                BackCubes.RemoveAt(i);
                i--;
            }
        }
    }

    void CreateBackCube(Vector2 dictance, Vector3 playerPos)
    {
        var position = Utilities.GeneratePosition(Session.Player.BlockPosition, dictance);
        var depth = F.Settings.BackgroundCubeDepth;
        position.y = UnityEngine.Random.Range(playerPos.y - depth.x, playerPos.y - depth.y);
        var cube = Instantiate(CubePrefab, transform);
        cube.transform.position = position;
        var scale = Vector3.one * UnityEngine.Random.Range(F.Settings.BackgroundCubeSize.x, F.Settings.BackgroundCubeSize.y);
        while (scale.sqrMagnitude > 0.3f && UnityEngine.Random.value < F.Settings.BackgroundCubeScaleChance)
        {
            scale /= 2f;
        }
        var meshRenderer = cube.GetComponentInChildren<MeshRenderer>();
        if (meshRenderer)
        {
            var r = UnityEngine.Random.value;
            var g = UnityEngine.Random.value;
            var b = UnityEngine.Random.value;
            var color = new Color(r, g, b);

            meshRenderer.material.color = color;
            //var block = new MaterialPropertyBlock();
            //meshRenderer.GetPropertyBlock(block);
            //block.SetColor("_Color", color);
            //meshRenderer.SetPropertyBlock(block);
        }
        cube.transform.localScale = scale;
        var rotation = new Vector3()
        {
            x = UnityEngine.Random.Range(0f, 180f),
            y = UnityEngine.Random.Range(0f, 180f),
            z = UnityEngine.Random.Range(0f, 180f)
        };
        cube.transform.Rotate(rotation);
        BackCubes.Add(cube);
    }

    public void Clear()
    {
        for (int i = 0; i < BackCubes.Count; i++)
        {
            Destroy(BackCubes[i]);
        }
        BackCubes.Clear();
    }
}
