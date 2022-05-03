using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button PlayButton;
    [SerializeField] Button ExitButton;
    [SerializeField] string WebplayerQuitURL = "http://google.com";


    void Awake()
    {
        PlayButton.onClick.RemoveAllListeners();
        PlayButton.onClick.AddListener(Play);
        ExitButton.onClick.RemoveAllListeners();
        ExitButton.onClick.AddListener(Exit);
    }

    void Play()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    void Exit()
    {
        Application.Quit();
//#if UNITY_EDITOR
//        UnityEditor.EditorApplication.isPlaying = false;
////#elif UNITY_WEBPLAYER
////        Application.OpenURL(WebplayerQuitURL);
//#else
//        Application.Quit();
//#endif
    }
}
