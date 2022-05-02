using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeExplode : MonoBehaviour
{
    public float Force = 300f;
    public float radius = 2f;
    private Rigidbody RB;
    private int RandomNum;
    
    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody>();

        RandomNum = Random.Range(-2,3);
        
    }

    private void Update()
    {
        RB.AddExplosionForce(Force, new Vector3(transform.position.x * RandomNum, transform.position.y * RandomNum, transform.position.z * RandomNum), radius);
    }
}
