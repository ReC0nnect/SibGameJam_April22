using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController
{
    SessionEntity Session;
    PortalEntity Portal;

    public Vector3 Position => Portal.Position;
    public bool IsActive => Portal.IsActivated;

    public PortalController(SessionEntity session)
    {
        Session = session;
    }

    public void CreatePortal()
    {
        if (Portal == null)
        {
            Portal = new PortalEntity(Session);

            for (int i = 0; i < Portal.PortalFrameLeft * 2; i++)
            {
                Session.Cube.AddPortalFrame();
            }
        }
    }

    public bool IsPortalPosition(Vector3 pos)
    {
        return Portal != null && (Portal.IsPortalFramePosition(pos) || Portal.IsEntrancePosition(pos));
    }

    public bool TryBuildPortal()
    {
        if (Session.Cube.HasPortalFrameCube)
        {
            return Portal.Build();
        }
        return false;
    }

    public void Clear()
    {
        Portal?.Destroy();
        Portal = null;
    }
}
