using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalVortex : MonoBehaviour
{
    SessionEntity Session;
    float AttractRange;

    public static PortalVortex Create(SessionEntity session, Vector3 position)
    {
        var vortex = Instantiate(F.Prefabs.PortalVortex);
        vortex.transform.position = position;
        vortex.Init(session);
        return vortex;
    }

    void Init(SessionEntity session)
    {
        Session = session;
        AttractRange = F.Settings.PortalVortexAttractRange;
    }

    void FixedUpdate()
    {
        var direction = transform.position - Session.Player.Position;
        if (direction.sqrMagnitude < AttractRange * AttractRange)
        {
            Session.Player.View.AddVelocity(direction.normalized * F.Settings.PortalVortexAttractForce);
        }
    }
}
