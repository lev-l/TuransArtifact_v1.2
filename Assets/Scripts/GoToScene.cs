using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour
{
    public KeyCode Key;
    public string Scene;

    private void Update()
    {
        if (Input.GetKeyDown(Key))
        {
            Load(Scene);
        }
    }

    public void Load(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
