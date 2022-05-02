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

    public void Update()
    {
        TryCreatePortal();
    }

    void TryCreatePortal()
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
        return Portal.IsPortalFramePosition(pos) || Portal.IsEntrancePosition(pos);
    }

    public bool TryBuildPortal()
    {
        if (Session.Cube.HasPortalFrameCube)
        {
            return Portal.Build();
        }
        return false;
    }
}
