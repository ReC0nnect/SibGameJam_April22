using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour
{
    [SerializeField] GameObject WinPanel;
    [SerializeField] Button WinButton;
    [SerializeField] TextMeshProUGUI WinTime;

    public static UI_Controller Instance;

    static Canvas MainCanvasCached;
    public static Canvas MainCanvas {
        get {
            if (!MainCanvasCached)
            {
                Instance = GameObject.FindObjectOfType<UI_Controller>();
                MainCanvasCached = Instance.GetComponent<Canvas>();
            }
            return MainCanvasCached;
        }
    }

    public void ShowWinPanel(float time)
    {
        if (TryGetComponent(out Time_Controller timeCtrl))
        {
            timeCtrl.DoPause();
        }
        WinPanel.SetActive(true);
        WinButton.onClick.RemoveAllListeners();
        WinButton.onClick.AddListener(GoToMainMenu);
        var timeSpan = System.TimeSpan.FromSeconds(time);
        WinTime.text = string.Format("{0:00}:{1:00}", timeSpan.TotalMinutes, timeSpan.Seconds);
    }

    public void GoToMainMenu()
    {
        HideWinPanel();
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void HideWinPanel()
    {
        WinPanel.SetActive(false);
    }
}
