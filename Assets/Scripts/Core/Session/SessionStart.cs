using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionStart : MonoBehaviour
{
    SessionEntity Session;

    void Awake()
    {
        Session = SessionEntity.Create();
    }

    void Update()
    {
        Session.Update();
    }
}
