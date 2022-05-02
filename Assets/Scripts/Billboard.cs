using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] bool OneStep;

    Camera Cam;
    void Start()
    {
        Cam = Camera.main;
        if (OneStep)
        {
            transform.rotation = Cam.transform.rotation;    
        }
    }

    void LateUpdate()
    {
        if (!OneStep)
        {
            transform.LookAt(Cam.transform);
        }
    }
}
