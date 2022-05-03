using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class UnitView : MonoBehaviour
{
    [SerializeField] Animation_Script Anim;
    [SerializeField] Player_Health Health;
    [SerializeField] SpriteRenderer View;
    [SerializeField] bool ControlFlip;
    public UnitEntity Entity { get; private set; }

    Rigidbody RigidbodyCached;
    Rigidbody Rigidbody {
        get {
            if (!RigidbodyCached)
            {
                RigidbodyCached = GetComponent<Rigidbody>();
            }
            return RigidbodyCached;
        }
    }

    public Animation_Script AnimScript => Anim;
    public Vector3 Position => transform.position;

    public void SetEntity(UnitEntity entity)
    {
        Entity = entity;
        if (Health)
        {
            Health.OnDeath += OnDeath;
        }
    }

    void OnDeath()
    {
        Health.OnDeath -= OnDeath;
        Entity.IsAlive = false;
        if (Anim)
        {
            Anim.KillPlayer();
        }
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void SetVelocity(Vector3 velocity)
    {
        Rigidbody.velocity = velocity;

        if (ControlFlip)
        {
            View.flipX = !(velocity.x > 0f && velocity.z > 0f 
                || velocity.normalized.x > -0.5f && velocity.z > 0f
                || velocity.x > 0f && velocity.normalized.z > -0.5f);
        }
    }

    public void AddVelocity(Vector3 velocity)
    {
        Rigidbody.velocity += velocity;
    }

    public void Stop()
    {
        Rigidbody.velocity = Vector3.zero;
    }
}
