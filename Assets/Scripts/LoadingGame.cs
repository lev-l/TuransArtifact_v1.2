using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingGame : MonoBehaviour
{
    public string SaveFileName;
    private LevelsControl _levels;

    void Start()
    {
        string saveFile = File.ReadAllText(SaveFileName + ".json");

        _levels = (LevelsControl)JsonUtility.FromJson(
                                                    saveFile,
                                                    typeof(LevelsControl)
                                                    );

        if(_levels.OpenedLevels.Count == 0)
        {
            SceneManager.LoadScene("FirstAct");
        }
        else
        {
            SceneManager.LoadScene(_levels.CurrentLevel);
        }
    }
}
