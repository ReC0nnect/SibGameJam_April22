using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalDownEntity : PortalEntity
{
    protected override Vector3[] FramePositions => new Vector3[]{
        new Vector3(-1.5f, 0f, -1.5f),
        new Vector3(-0.5f, 0f, -1.5f),
        new Vector3( 0.5f, 0f, -1.5f),
        new Vector3( 1.5f, 0f, -1.5f),
        new Vector3(-1.5f, 0f, -0.5f),
        new Vector3( 1.5f, 0f, -0.5f),
        new Vector3(-1.5f, 0f,  0.5f),
        new Vector3( 1.5f, 0f,  0.5f),
        new Vector3(-1.5f, 0f,  1.5f),
        new Vector3(-0.5f, 0f,  1.5f),
        new Vector3( 0.5f, 0f,  1.5f),
        new Vector3( 1.5f, 0f,  1.5f) 
    };

    protected override Vector3[] EntrancePositions => new Vector3[]{
        new Vector3( 0.5f, 0f,  0.5f),
        new Vector3( 0.5f, 0f, -0.5f),
        new Vector3(-0.5f, 0f,  0.5f),
        new Vector3(-0.5f, 0f, -0.5f)
    };

    public override PortalFrame PortalFramePrefab => F.Prefabs.PortalDownFrame;
    protected override Vector3 OffsetPosition => new Vector3(0.5f, 0f, 0.5f);

    public PortalDownEntity(SessionEntity session) : base(session)
    {

    }

    protected override void SetUpEntrance()
    {
        for (int i = 0; i < EntrancePositions.Length; i++)
        {
            var pos = Position;
            pos.y = 0f;
            EntranceBlocks.Add(pos + EntrancePositions[i]);
        }
    }

    protected override void ActivatePortal()
    {
        IsActivated = true;
        PortalVortex = GameObject.Instantiate(F.Prefabs.PortalDownVortex);
        PortalVortex.transform.position = NormalizedPosition;
        PortalVortex.Init(Session);
    }
}
