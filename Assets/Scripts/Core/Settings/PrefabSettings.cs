using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[CreateAssetMenu(fileName = "PrefabSettings", menuName = "Create Prefab Settings")]
public class PrefabSettings : ScriptableObject
{
    public UnitView Enemy_1;
    public UnitView Enemy_2;
    public UnitView Enemy_3;
    public UnitView Enemy_4;
    public UnitView Enemy_5;
    public UnitView Enemy_6;


    public UnitView Follower;
    public MysteryCube Cube;
    public MysteryCube FreeCube;
    public MysteryCube PortalFrameCube;
    public PortalFrame PortalFrame;
    public PortalVortex PortalVortex;
}
