using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelsLoader : MonoBehaviour
{
    public string LoadLevel;
    public Vector2 LoadPosition;
    public LoadingPosition PositionContainer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        File.WriteAllText(PositionContainer.FileName + ".json",
                                JsonUtility.ToJson(LoadPosition));
        SceneManager.LoadScene(LoadLevel);
    }
}
