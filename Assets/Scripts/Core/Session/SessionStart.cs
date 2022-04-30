using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionStart : MonoBehaviour
{
    void Awake()
    {
        SessionEntity.Create();
    }
}
