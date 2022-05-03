using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour
{
    [SerializeField] GameObject WinPanel;
    [SerializeField] Button WinButton;

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

    public void ShowWinPanel()
    {
        WinPanel.SetActive(true);
        WinButton.onClick.RemoveAllListeners();
        WinButton.onClick.AddListener(GoToMainMenu);
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
