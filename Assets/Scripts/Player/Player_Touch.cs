using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Touch : MonoBehaviour
{
    // Start is called before the first frame update
    private Player_Health Player_HP;

    void Start()
    {
        Player_HP = gameObject.GetComponentInParent<Player_Health>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    
}
