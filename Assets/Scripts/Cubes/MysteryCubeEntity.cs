using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryCubeEntity
{
    public SessionEntity Session { get; }

    MysteryCube View;

    public MysteryCubeEntity(SessionEntity session)
    {
        Session = session;
    }

    public Vector3 Position => View.Position;

    public void CreateView(Transform parent, Vector3 position)
    {
        View = GameObject.Instantiate(F.Prefabs.Cube, parent);
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
