using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public int SpeedRotate = 250;
   
    void Update()
    {
        transform.Rotate(Vector3.forward * SpeedRotate * Time.deltaTime);
    }
}
