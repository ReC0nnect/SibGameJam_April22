using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CubeController
{
    SessionEntity Session { get; }

    List<MysteryCubeEntity> FreeCubes;
    List<MysteryCubeInfo> Cubes;
    List<Vector3> Sockets;

    Vector3[] TransitionMatrix = new Vector3[4]
    {
        new Vector3(0f, 0f, 1f),
        new Vector3(1f, 0f, 0f),
        new Vector3(0f, 0f, -1f),
        new Vector3(-1f, 0f, 0f)
    };


    Transform CubesParentCached;
    Transform CubesParent {
        get {
            if (!CubesParentCached)
            {
                var container = GameObject.FindObjectOfType<CubeContainer>();
                if (container)
                {
                    CubesParentCached = container.transform;
                }
            }
            return CubesParentCached;
        }
    }

    public CubeController(SessionEntity session)
    {
        Session = session;

        CreateCubes();
        CreateFreeCubes();

        Sockets = new List<Vector3>();
        UpdateSockets();

    }

    public void AddCube()
    {

    }

    void CreateCubes()
    {
        Cubes = new List<MysteryCubeInfo>();
        CreateCube(Vector3.zero);

        for (float r = 1f; r < F.Settings.CubeRadius; r += 0.5f)
        {
            for (int j = 0; j < Cubes.Count; j++)
            {
                for (int n = 0; n < TransitionMatrix.Length; n++)
                {
                    var pos = Cubes[j].Position + TransitionMatrix[n];
                    if (pos.sqrMagnitude > r * r)
                    {
                        continue;
                    }
                    if (!Cubes.Exists(c => c.Position == pos))
                    {
                        CreateCube(pos);
                    }
                }
            }
        }
    }

    void UpdateSockets()
    {
        Sockets.Clear();
        for (int i = 0; i < Cubes.Count; i++)
        {
            for (int n = 0; n < TransitionMatrix.Length; n++)
            {
                var pos = Cubes[i].Position + TransitionMatrix[n];
                if (!Cubes.Exists(c => c.Position == pos))
                {
                    if (!TryAddCube(pos, true))
                    {
                        Sockets.Add(pos);
                    }
                }
            }
        }
    }

    bool TryAddCube(Vector3 position, bool updateSockets = false)
    {
        var freeCube = FreeCubes.Find(fc => (fc.Position - position).sqrMagnitude < 0.1f);
        if (freeCube != null)
        {
            freeCube.SetCaptured(true);
            Cubes.Add(new MysteryCubeInfo(freeCube, freeCube.Position));
            FreeCubes.Remove(freeCube);
            AddFreeCube();

            if (updateSockets)
            {
                for (int n = 0; n < TransitionMatrix.Length; n++)
                {
                    var pos = freeCube.Position + TransitionMatrix[n];
                    if (!Cubes.Exists(c => c.Position == pos))
                    {
                        Sockets.Add(pos);
                    }
                }
            }
            return true;
        }
        return false;
    }

    void CreateCube(Vector3 pos)
    {
        var cube = new MysteryCubeEntity(Session);
        cube.CreateView(CubesParent, pos);
        Cubes.Add(new MysteryCubeInfo(cube, pos));
    }

    #region FreeCubes
    void CreateFreeCubes()
    {
        FreeCubes = new List<MysteryCubeEntity>();
        for (int i = 0; i < F.Settings.FreeCubeCount; i++)
        {
            AddFreeCube();
        }
    }

    void AddFreeCube()
    {
        var position = GeneratePosition();
        var cube = new MysteryCubeEntity(Session);
        cube.CreateView(CubesParent, position);
        cube.SetCaptured(false);
        FreeCubes.Add(cube);
    }

    void UpdateFreeCubes()
    {
        var sqrMaxDistance = F.Settings.FreeCubeMaxDistance * F.Settings.FreeCubeMaxDistance;
        for (int i = 0; i < FreeCubes.Count; i++)
        {
            if ((FreeCubes[i].Position - Session.Player.Position).sqrMagnitude > sqrMaxDistance)
            {
                FreeCubes[i].Destroy();
                FreeCubes.RemoveAt(i);
                i--;
                AddFreeCube();
            }
        }
    }

    Vector3 GeneratePosition()
    {
        var direction = new Vector3()
        {
            x = UnityEngine.Random.Range(-1f, 1f),
            y = 0f,
            z = UnityEngine.Random.Range(-1f, 1f)
        };
        var position = Session.Player.Position + direction.normalized * UnityEngine.Random.Range(F.Settings.FreeCubeRange.x, F.Settings.FreeCubeRange.y);
        position.x = Mathf.Round(position.x);
        position.y = 0f;
        position.z = Mathf.Round(position.z);
        return position;
    }
    #endregion

    public void Update()
    {
        MoveCubes();
        UpdateFreeCubes();

        if (Input.GetKeyDown(KeyCode.Space) && Session.Enemy.HasEnemy)
        {
            Shoot();
        }
    }

    void MoveCubes()
    {
        var radiusInBlocks = (Mathf.Sqrt(Cubes.Count)) / 2f + 1f;
        var sqrRadius = radiusInBlocks * radiusInBlocks;
        for (int i = 0; i < Cubes.Count; i++)
        {
            var sqrMagnitude = (Cubes[i].Position - Session.Player.Position).sqrMagnitude;
            if (sqrMagnitude > sqrRadius)
            {
                MoveCubeByIndex(i, sqrMagnitude);
            }
        }
    }

    void MoveCubeByIndex(int index, float magnitude)
    {
        var nearestSocketPos = Sockets.OrderBy(s => (Session.Player.Position - s).sqrMagnitude).First();

        if ((Session.Player.Position - nearestSocketPos).sqrMagnitude < magnitude)
        {
            Cubes[index].Cube.UpdatePosition(nearestSocketPos);
            Cubes[index].Position = nearestSocketPos;

            UpdateSockets();
        }
    }

    void Shoot()
    {
        var farrestCube = Cubes.OrderByDescending(c => (Session.Player.Position - c.Position).sqrMagnitude).First();
        
        Cubes.Remove(farrestCube);
        farrestCube.Cube.Shoot(Session.Enemy.GetNearestOfPlayer());
    }
}
