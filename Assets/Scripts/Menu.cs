using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : OpenClose
{
    public void Play()
    {
        SceneManager.LoadScene("GameLoading");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void OpenSetting()
    {
        StopAllCoroutines();
        StartCoroutine(MoveTo(OpenPosition));
    }

    public void CloseSettings()
    {
        StartCoroutine(MoveTo(_defoultPosition));
    }
}
