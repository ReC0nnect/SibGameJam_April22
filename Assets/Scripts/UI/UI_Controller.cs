using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Controller : MonoBehaviour
{
    static Canvas MainCanvasCached;
    public static Canvas MainCanvas {
        get {
            if (!MainCanvasCached)
            {
                var instance = GameObject.FindObjectOfType<UI_Controller>();
                MainCanvasCached = instance.GetComponent<Canvas>();
            }
            return MainCanvasCached;
        }
    }
}
