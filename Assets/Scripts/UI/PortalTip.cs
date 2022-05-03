using System;
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

    bool Activated;

    public void Init(PortalEntity portal)
    {
        Portal = portal;
        Camera = Camera.main;
        View.gameObject.SetActive(false);

        Portal.Session.Player.OnFallingFinished += ShowTip;
        if (ShowCoroutine != null)
        {
            StopCoroutine(ShowCoroutine);
        }
        ShowCoroutine = StartCoroutine(ShowTipDelay());
    }

    void OnDestroy()
    {
        if (Portal != null && Portal.Session != null && Portal.Session.Player != null)
        {
            Portal.Session.Player.OnFallingFinished -= ShowTip;
        }
    }

    void ShowTip()
    {
        Activated = false;
        if (ShowCoroutine != null)
        {
            StopCoroutine(ShowCoroutine);
        }
        ShowCoroutine = StartCoroutine(ShowTipDelay());
    }

    Coroutine ShowCoroutine;
    IEnumerator ShowTipDelay()
    {
        yield return new WaitForSeconds(F.Settings.PortalTipDelay);
        Activated = true;
        ShowCoroutine = null;
    }

    void Update()
    {
        if (Activated)
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
}
