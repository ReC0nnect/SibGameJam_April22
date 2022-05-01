using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryCubeInfo
{
    public Vector3 Position;
    public MysteryCubeEntity Cube;

    public MysteryCubeInfo(MysteryCubeEntity cube, Vector3 position)
    {
        Cube = cube;
        Position = position;
    }
}
