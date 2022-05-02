using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalEntity
{
    readonly Vector3[] FramePositions = new Vector3[]{
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

    PortalVortex PortalVortex;
    List<PortalFrame> Frames { get; }
    List<Vector3> EntranceBlocks { get; }
    public SessionEntity Session { get; }
    public Vector3 Position { get; }
    public int PortalFrameLeft => F.Settings.PortalFrameCount - Frames.Count;
    public bool IsActivated { get; private set; }

    Transform PortalParentCached;
    Transform PortalParent {
        get {
            if (!PortalParentCached)
            {
                var container = GameObject.FindObjectOfType<PortalContainer>();
                if (container)
                {
                    PortalParentCached = container.transform;
                }
            }
            return PortalParentCached;
        }
    }

    public PortalEntity(SessionEntity session)
    {
        Session = session;
        Frames = new List<PortalFrame>(F.Settings.PortalFrameCount);
        EntranceBlocks = new List<Vector3>(F.Settings.PortalEntranceCount);

        var portalPosition = Utilities.GeneratePosition(Session.Player.Position, F.Settings.PortalSpawnRange);
        Position = portalPosition + new Vector3(0.5f, 0f, 0.5f);

        EntranceBlocks.Add(new Vector3(Position.x + 0.5f, 0f, Position.x + 0.5f));
        EntranceBlocks.Add(new Vector3(Position.x + 0.5f, 0f, Position.x - 0.5f));
        EntranceBlocks.Add(new Vector3(Position.x - 0.5f, 0f, Position.x + 0.5f));
        EntranceBlocks.Add(new Vector3(Position.x - 0.5f, 0f, Position.x - 0.5f));

        int frameStartCount = (int)UnityEngine.Random.Range(F.Settings.PortalFrameStartCount.x, F.Settings.PortalFrameStartCount.y);
        for (int i = 0; i < frameStartCount; i++)
        {
            var framePos = GenerateFramePosition(out var idx);
            var frame = GameObject.Instantiate(F.Prefabs.PortalFrame, PortalParent);
            frame.Init(this, framePos, idx);
            Frames.Add(frame);
        }
    }

    public bool IsPortalFramePosition(Vector3 posision)
    {
        var frame = Frames.Find(f => f.Position == posision);
        return frame != null;
    }

    public bool IsEntrancePosition(Vector3 posision)
    {
        return IsActivated && EntranceBlocks.Contains(posision);
    }

    public bool Build()
    {
        var frames = Session.Cube.PortalFrames;
        for (int i = 0; i < F.Settings.PortalFrameCount; i++)
        {
            if (Frames.Exists(f => f.Index == i)) { continue; }

            if (frames[0].View.TryGetComponent(out PortalFrame portalFrame))
            {
                portalFrame.transform.parent = PortalParent;
                frames[0].UpdatePosition(Position + FramePositions[i]);
                portalFrame.SetIndex(i);
                Frames.Add(portalFrame);
                Session.Cube.RemoveFromList(frames[0]);
                frames.RemoveAt(0);
            }

            if (PortalFrameLeft == 0)
            {
                ActivatePortal();
                return true;
            }
            if (frames.Count == 0)
            {
                return true;
            }
        }
        return false;
    }

    void ActivatePortal()
    {
        IsActivated = true;
        PortalVortex = PortalVortex.Create(Session, Position);
        Debug.LogWarning("Portal activated");
        //TODO 
    }

    Vector3 GenerateFramePosition(out int id)
    {
        Vector3 result;
        do
        {
            id = (int)UnityEngine.Random.Range(1f, F.Settings.PortalFrameCount);
            result = Position + FramePositions[id];
        } while (Frames.Exists(f => f.Position == result));

        return result;
    }
}
