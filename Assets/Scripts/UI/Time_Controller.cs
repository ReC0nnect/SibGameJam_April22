using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Time_Controller : MonoBehaviour
{
    public float slowdownFactor = 0.05f;
    public float slowdownLength = 2f;

    public GameObject DeadMenu;

    public bool IsPaused;
    // Start is called before the first frame update
    
    
    
    public void DoPause()
    {
        Time.timeScale = 0;
        IsPaused = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (IsPaused == true)
            return;


        Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
    }

    void OnDestroy()
    {
        Time.timeScale = 1;
    }

    public void ShowDeadMenu()
    {
        DeadMenu.SetActive(true);
    }

    public void HideDeadMenu()
    {
        DeadMenu.SetActive(false);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }

    public void UnPause()
    {
        StartCoroutine(OffPause());
    }

    IEnumerator OffPause()
    {
        Time.timeScale = 1;
        yield return new WaitForSeconds(0.8f);

        IsPaused = false;
    }
}
