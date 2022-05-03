using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalUpEntity : PortalEntity
{
    protected override Vector3[] FramePositions => new Vector3[]{
        new Vector3(0f, 0f, -1.5f),
        new Vector3(0f, 0f, -0.5f),
        new Vector3(0f, 0f,  0.5f),
        new Vector3(0f, 0f,  1.5f),
        new Vector3(0f, 1f, -1.5f),
        new Vector3(0f, 1f,  1.5f),
        new Vector3(0f, 2f, -1.5f),
        new Vector3(0f, 2f,  1.5f),
        new Vector3(0f, 3f, -1.5f),
        new Vector3(0f, 3f,  1.5f),
        new Vector3(0f, 4f, -1.5f),
        new Vector3(0f, 4f, -0.5f),
        new Vector3(0f, 4f,  0.5f),
        new Vector3(0f, 4f,  1.5f)
    };

    protected override Vector3[] EntrancePositions => new Vector3[]{
        new Vector3(0f, 1f,  0.5f),
        new Vector3(0f, 1f, -0.5f),
        new Vector3(0f, 2f,  0.5f),
        new Vector3(0f, 2f, -0.5f),
        new Vector3(0f, 3f,  0.5f),
        new Vector3(0f, 3f, -0.5f)
    };

    public override PortalFrame PortalFramePrefab => F.Prefabs.PortalUpFrame;
    protected override Vector3 OffsetPosition => new Vector3(0f, 0f, 0.5f);

    public PortalUpEntity(SessionEntity session) : base(session)
    {

    }

    protected override void SetUpEntrance()
    {
        for (int i = 0; i < EntrancePositions.Length; i++)
        {
            var pos = Position;
            pos.x = 0f;
            EntranceBlocks.Add(pos + EntrancePositions[i]);
        }
    }

    protected override void ActivatePortal()
    {
        IsActivated = true;

        PortalVortex = GameObject.Instantiate(F.Prefabs.PortalUpVortex);
        var position = NormalizedPosition;
        position.y += 1.5f;
        PortalVortex.transform.position = position;
        PortalVortex.Init(Session);
    }
}

