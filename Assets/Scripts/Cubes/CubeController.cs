using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CubeController
{
    readonly Vector3[] TransitionMatrix = new Vector3[4]
    {
        new Vector3(0f, 0f, 1f),
        new Vector3(1f, 0f, 0f),
        new Vector3(0f, 0f, -1f),
        new Vector3(-1f, 0f, 0f)
    };

    List<MysteryCubeEntity> FreeCubes;
    List<MysteryCubeInfo> Cubes;
    List<Vector3> Sockets;

    SessionEntity Session { get; }
    public bool HasPortalFrameCube => Cubes.Exists(c => c.Cube.IsPortalFrame);

    public List<MysteryCubeEntity> PortalFrames => Cubes.Where(c => c.Cube.IsPortalFrame)
                                                        .Select(c => c.Cube)
                                                        .ToList();

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

        CubesParent.position = Vector3.down * 0.5f - Vector3.up * (session.LevelNumber * F.Settings.LevelDistance);

        CreateCubes();
        CreateFreeCubes();

        Sockets = new List<Vector3>();
        UpdateSockets();
    }

    void CreateCubes()
    {
        Cubes = new List<MysteryCubeInfo>();
        var playerPos = Session.Player.BlockPosition;
        CreateCube(playerPos);

        for (float r = 1f; r < F.Settings.CubeRadius; r += 0.5f)
        {
            for (int j = 0; j < Cubes.Count; j++)
            {
                for (int n = 0; n < TransitionMatrix.Length; n++)
                {
                    var pos = Cubes[j].Position + TransitionMatrix[n];
                    if ((playerPos - pos).sqrMagnitude > r * r)
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
        BeautySpawn();
    }

    void BeautySpawn()
    {
        for (int i = 1; i < Cubes.Count; i++)
        {
            var position = Session.Player.NormalizedPosition;
            position.x += UnityEngine.Random.Range(-16f, 16f);
            position.y = 20f;
            position.z += UnityEngine.Random.Range(-16f, 16f);
            Cubes[i].Cube.SetPosition(position);
            Cubes[i].Cube.LockPositionChanging = true;
            Cubes[i].Cube.UpdatePosition(Cubes[i].Position, F.Settings.CubeMovingSpeed);
        }
    }

    public void Clear()
    {
        foreach (var freeCube in FreeCubes)
        {
            freeCube.Destroy();
        }
        FreeCubes.Clear();
        foreach (var cube in Cubes)
        {
            cube.Cube.Destroy();
        }
        Cubes.Clear();
        Sockets.Clear();
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
                    if (Session.Portal != null && Session.Portal.IsPortalPosition(pos))
                    {
                        if (Session.Portal.IsActive || Session.Portal.TryBuildPortal())
                        {
                            break;
                        }
                    }
                    else if (!TryAddCube(pos, true))
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

    MysteryCubeEntity CreateCube(Vector3 pos)
    {
        var cube = new MysteryCubeEntity(Session);
        cube.CreateView(CubesParent, F.Prefabs.Cube, pos);
        Cubes.Add(new MysteryCubeInfo(cube, pos));
        return cube;
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

    public MysteryCubeEntity AddFreeCube(Vector3? position = null)
    {
        if (!position.HasValue)
        {
            position = Utilities.GeneratePosition(Session.Player.BlockPosition, F.Settings.FreeCubeRange);
        }
        var cube = new MysteryCubeEntity(Session);
        cube.CreateView(CubesParent, F.Prefabs.FreeCube, position.Value);
        cube.SetCaptured(false);
        FreeCubes.Add(cube);
        return cube;
    }

    void UpdateFreeCubes()
    {
        var sqrMaxDistance = F.Settings.FreeCubeMaxDistance * F.Settings.FreeCubeMaxDistance;
        var playerPosition = Session.Player.NormalizedPosition;
        for (int i = 0; i < FreeCubes.Count; i++)
        {
            if ((FreeCubes[i].Position - playerPosition).sqrMagnitude > sqrMaxDistance)
            {
                if (FreeCubes[i].IsPortalFrame)
                {
                    AddPortalFrame();
                }
                else
                {
                    AddFreeCube();
                }
                FreeCubes[i].Destroy();
                FreeCubes.RemoveAt(i);
                i--;
            }
        }
    }
    #endregion
    
    #region PortalFrame
    public void AddPortalFrame(Vector3? position = null)
    {
        if (!position.HasValue)
        {
            do
            {
                position = Utilities.GeneratePosition(Session.Player.BlockPosition, F.Settings.PortalFrameSpawnRange);

            } while (!IsEmptyPosition(position.Value));
        }
        var cube = new MysteryCubeEntity(Session);
        cube.CreateView(CubesParent, Session.Portal.PortalFrameCubePrefab, position.Value);
        FreeCubes.Add(cube);
    }
    #endregion

    public bool IsEmptyPosition(Vector3 position)
    {
        return !FreeCubes.Exists(c => c.Position == position)
               && (Session.Portal.Position - position).sqrMagnitude > F.Settings.PortalFrameForbiddenDistanceToPortal * F.Settings.PortalFrameForbiddenDistanceToPortal;
    }

    public void Update()
    {
        if (!Session.Player.IsFalling)
        {
            MoveCubes();
            UpdateFreeCubes();

            if (Input.GetKeyDown(KeyCode.Space) && Session.Enemy.HasEnemy)
            {
                Shoot();
            }
        }
    }

    void MoveCubes()
    {
        var radiusInBlocks = (Mathf.Sqrt(Cubes.Count)) / 2f + 1f;
        var sqrRadius = radiusInBlocks * radiusInBlocks;
        var playerNormalizedPosition = Session.Player.NormalizedPosition;
        for (int i = 0; i < Cubes.Count; i++)
        {
            var sqrMagnitude = (Cubes[i].Position - playerNormalizedPosition).sqrMagnitude;
            if (sqrMagnitude > sqrRadius)
            {
                MoveCubeByIndex(i, sqrMagnitude);
            }
        }
    }

    void MoveCubeByIndex(int index, float magnitude)
    {
        var playerNormalizedPosition = Session.Player.NormalizedPosition;
        var nearestSocketPos = Sockets.OrderBy(s => (playerNormalizedPosition - s).sqrMagnitude).First();

        if ((playerNormalizedPosition - nearestSocketPos).sqrMagnitude < magnitude)
        {
            Cubes[index].Cube.UpdatePosition(nearestSocketPos, F.Settings.CubeMovingSpeed);
            Cubes[index].Position = nearestSocketPos;

            UpdateSockets();
        }
    }

    public void StartFallCube(Vector3 fromPosition, Vector3 toPosition)
    {
        var cube = CreateCube(fromPosition);
        cube.UpdatePosition(toPosition, F.Settings.CubeFallingSpeed);
    }

    void Shoot()
    {
        if (Cubes.Count > 0)
        {
            var farrestCube = Cubes.Where(c => !c.Cube.IsPortalFrame)
                               .OrderByDescending(c => (Session.Player.NormalizedPosition - c.Position).sqrMagnitude)
                               .First();
            RemoveFromList(farrestCube);
            farrestCube.Cube.Shoot(Session.Enemy.GetNearestOfPlayer());
        }
    }

    public void RemoveFromList(MysteryCubeEntity cube)
    {
        Cubes.RemoveAll(c => c.Cube == cube);
    }

    public void RemoveFromList(MysteryCubeInfo cube)
    {
        Cubes.Remove(cube);
    }
}
