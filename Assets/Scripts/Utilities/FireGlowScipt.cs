using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGlowScipt : MonoBehaviour {

    Renderer rend;
    float colInt;
    Color c;

    public float minColInt = 0.4f, maxColInt = 0.5f;

	// Use this for initialization
	void Start () {
        rend = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        colInt = Random.Range(minColInt, maxColInt);
        
        c = rend.material.color;
        c.a = colInt;
        rend.material.color = c;
	}
}
