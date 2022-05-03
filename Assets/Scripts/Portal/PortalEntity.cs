using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PortalEntity
{
    protected PortalVortex PortalVortex;
    protected abstract Vector3[] FramePositions { get; }
    protected abstract Vector3[] EntrancePositions { get; }

    public Vector3 Position { get; }
    public Vector3 NormalizedPosition => Position + Vector3.down * Session.LevelNumber * F.Settings.LevelDistance;
    public int PortalFrameCount => FramePositions.Length;
    public int PortalFrameLeft => PortalFrameCount - Frames.Count;
    public int PortalEntranceCount => EntrancePositions.Length;
    public int PortalFrameSpawnCount => F.Settings.PortalFrameSpawnExtraCount;

    abstract protected Vector3 OffsetPosition { get; }
    abstract public PortalFrame PortalFramePrefab { get; }

    protected List<Vector3> EntranceBlocks { get;}
    protected List<PortalFrame> Frames { get;}

    public SessionEntity Session { get; }
    public bool IsActivated { get; protected set; }

    Transform PortalParentCached;
    protected Transform PortalParent {
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
        Frames = new List<PortalFrame>(PortalFrameCount);
        EntranceBlocks = new List<Vector3>(PortalEntranceCount);

        PortalParent.position = Vector3.down * 0.5f - Vector3.up * (session.LevelNumber * F.Settings.LevelDistance);

        var portalPosition = Utilities.GeneratePosition(Session.Player.BlockPosition, F.Settings.PortalSpawnRange);
        Position = portalPosition + OffsetPosition;

        SetUpEntrance();

        int frameStartCount = (int)Random.Range(F.Settings.PortalFrameStartCount.x, F.Settings.PortalFrameStartCount.y);
        for (int i = 0; i < frameStartCount; i++)
        {
            var framePos = GenerateFramePosition(out var idx);
            var frame = GameObject.Instantiate(PortalFramePrefab, PortalParent);
            frame.Init(this, framePos, idx);
            Frames.Add(frame);
        }
    }

    public void Destroy()
    {
        if (PortalVortex)
        {
            GameObject.Destroy(PortalVortex.gameObject);
        }
        for (int i = 0; i < Frames.Count; i++)
        {
            GameObject.Destroy(Frames[i].gameObject);
        }
        Frames.Clear();
        EntranceBlocks.Clear();
    }


    abstract protected void SetUpEntrance();
    abstract protected void ActivatePortal();

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
        for (int i = 0; i < PortalFrameCount; i++)
        {
            if (Frames.Exists(f => f.Index == i)) { continue; }

            if (frames[0].View.TryGetComponent(out PortalFrame portalFrame))
            {
                portalFrame.transform.parent = PortalParent;
                frames[0].UpdatePosition(Position + FramePositions[i], F.Settings.CubeMovingSpeed);
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

    Vector3 GenerateFramePosition(out int id)
    {
        Vector3 result;
        do
        {
            id = (int)Random.Range(1f, PortalFrameCount);
            result = Position + FramePositions[id];
        } while (Frames.Exists(f => f.Position == result));

        return result;
    }
}
