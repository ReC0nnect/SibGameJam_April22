using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortalTip : MonoBehaviour
{
    [SerializeField] SpriteRenderer View;
    [SerializeField] Vector3 Offset;

    Camera Camera;
    PortalEntity Portal;

    public void Init(PortalEntity portal)
    {
        Portal = portal;
        Camera = Camera.main;
    }

    void Update()
    {
        var screenPosition = Camera.WorldToScreenPoint(Portal.NormalizedPosition);
        var rect = UI_Controller.MainCanvas.pixelRect;
        if (!rect.Contains(screenPosition) && !Portal.Session.Player.IsFalling)
        {
            View.gameObject.SetActive(true);
            transform.position = Portal.Session.Player.Position + Offset;
            View.transform.LookAt(Portal.NormalizedPosition + Offset);
            View.transform.Rotate(Vector3.right, 90f);
        }
        else
        {
            View.gameObject.SetActive(false);
        }
    }
}
