using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CubeController
{
    SessionEntity Session { get; }

    //List<MisteryCubeEntity> Cubes;
    List<MysteryCubeInfo> Cubes;
    List<Vector3> Sockets;


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

        Sockets = new List<Vector3>();
        UpdateSockets();
    }

    void CreateCubes()
    {
        Cubes = new List<MysteryCubeInfo>();
        CreateCube(Vector3.zero);

        var nearest = new Vector3[] 
        { 
            new Vector3(0f, 0f, 1f), 
            new Vector3(1f, 0f, 0f),
            new Vector3(0f, 0f, -1f),
            new Vector3(-1f, 0f, 0f)
        };

        for (float r = 1f; r < F.Settings.CubeRadius; r += 0.5f)
        {
            for (int j = 0; j < Cubes.Count; j++)
            {
                for (int n = 0; n < nearest.Length; n++)
                {
                    var pos = Cubes[j].Position + nearest[n];
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
        var nearest = new Vector3[]
        {
            new Vector3(0f, 0f, 1f),
            new Vector3(1f, 0f, 0f),
            new Vector3(0f, 0f, -1f),
            new Vector3(-1f, 0f, 0f)
        };

        for (int i = 0; i < Cubes.Count; i++)
        {
            for (int n = 0; n < nearest.Length; n++)
            {
                var pos = Cubes[i].Position + nearest[n];
                if (!Cubes.Exists(c => c.Position == pos))
                {
                    Sockets.Add(pos);
                }
            }
        }
    }

    void CreateCube(Vector3 pos)
    {
        var cube = new MysteryCubeEntity(Session);
        cube.CreateView(CubesParent, pos);
        Cubes.Add(new MysteryCubeInfo(cube, pos));
    }

    public void Update()
    {
        var sqrRadius = F.Settings.CubeRadius * F.Settings.CubeRadius;
        for (int i = 0; i < Cubes.Count; i++)
        {
            if ((Cubes[i].Position - Session.Player.Position).sqrMagnitude > sqrRadius)
            {
                MoveCubeByIndex(i);
            }
        }
    }

    void MoveCubeByIndex(int index)
    {
        var nearestSocketPos = Sockets.OrderBy(s => (Session.Player.Position - s).sqrMagnitude).First();

        Cubes[index].Cube.UpdatePosition(nearestSocketPos);
        Cubes[index].Position = Cubes[index].Cube.Position;

        UpdateSockets();
    }
}
