using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryCubeEntity
{
    public SessionEntity Session { get; }
    public MysteryCube View { get; private set; }

    public MysteryCubeEntity(SessionEntity session)
    {
        Session = session;
    }

    public Vector3 Position => View.Position;
    public bool IsPortalFrame => View.IsPortalFrame;

    public void CreateView(Transform parent, MysteryCube prefab, Vector3 position)
    {
        View = GameObject.Instantiate(prefab, parent);
        View.transform.localPosition = position;
        View.Init(this);
    }

    public void UpdatePosition(Vector3 position)
    {
        View.SetPosition(position);
    }

    public void Destroy()
    {
        GameObject.Destroy(View.gameObject);
    }

    public void Shoot(UnitEntity target)
    {
        View.Shoot(target);
    }

    public void SetCaptured(bool state)
    {
        View.ChangeMaterial(state);
    }
}
