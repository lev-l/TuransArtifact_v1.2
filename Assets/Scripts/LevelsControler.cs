using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelsControler : MonoBehaviour
{
    public string SaveFileName;
    public LoadingPosition StartPosition;
    public Ables PlayerAbles;
    private LevelsControl _levels;

    private void Awake()
    {
        string saveFile = File.ReadAllText(SaveFileName + ".json");

        // load
        _levels = (LevelsControl)JsonUtility.FromJson
                                                (
                                                saveFile,
                                                typeof(LevelsControl)
                                                );

        // edit
        Scene currentScene = SceneManager.GetActiveScene();
        _levels.OpenLevel(currentScene.name);
        _levels.CurrentLevel = currentScene.name;

        // update ables
        PlayerAbles.Fly = LevelsContains("SecondLevel");
        PlayerAbles.PhysicsTeleportation = LevelsContains("ThirdLevel");
        PlayerAbles.Teleportation = LevelsContains("FourthLevel");
        PlayerAbles.SpearJump = LevelsContains("FifthLevel");

        // write
        File.WriteAllText(SaveFileName + ".json", JsonUtility.ToJson(_levels));

        // load position
        GameObject.FindGameObjectWithTag("Player").transform.position
            = (Vector3)JsonUtility.FromJson(File.ReadAllText(StartPosition.FileName + ".json"),
                                                                                typeof(Vector3));
    }

    private bool LevelsContains(string level)
    {
        return _levels.OpenedLevels.Contains(level);
    }
}
