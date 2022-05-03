using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalVortex : MonoBehaviour
{
    [SerializeField] string ShowSound = "PortalActivated";
    SessionEntity Session;
    float AttractRange;

    public void Init(SessionEntity session)
    {
        Session = session;
        AttractRange = F.Settings.PortalVortexAttractRange;
        Session.SFX.Play(ShowSound);
    }

    void FixedUpdate()
    {
        var direction = transform.position - Session.Player.Position;
        if (direction.sqrMagnitude < AttractRange * AttractRange)
        {
            Session.Player.View.AddVelocity(direction.normalized * F.Settings.PortalVortexAttractForce);

            if (direction.sqrMagnitude < 1f && !Session.Player.IsFalling)
            {
                Session.GoNextLevel();
            }
        }
    }
}
