using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsFile
{
    public float MusicVolume;
    public float SoundsVolume;
    private string _saveFileName;

    public SettingsFile(string fileName)
    {
        _saveFileName = fileName;
        SettingsFile savedSettings
                = JsonUtility.FromJson<SettingsFile>(File.ReadAllText(_saveFileName + ".json"));
        MusicVolume = savedSettings.MusicVolume;
        SoundsVolume = savedSettings.SoundsVolume;
    }

    public void SetVolumes(float music, float sounds)
    {
        MusicVolume = music;
        SoundsVolume = sounds;
    }

    public void WriteToJson()
    {
        File.WriteAllText(_saveFileName + ".json", JsonUtility.ToJson(this));
    }
}

public class Settings : MonoBehaviour
{
    public string SaveFileName;
    public AudioMixer Mixer;
    private Slider _slider0, _slider1;
    private SettingsFile _saveFile;

    void Start()
    {
        Slider[] sliders = GetComponentsInChildren<Slider>();
        _slider0 = sliders[0];
        _slider1 = sliders[1];

        _saveFile = new SettingsFile(SaveFileName);
        _slider0.value = _saveFile.MusicVolume;
        _slider1.value = _saveFile.SoundsVolume;

        // register manual because we need delay before start observing events
        _slider0.onValueChanged.AddListener(delegate { ValueChangedMusic(); });
        _slider1.onValueChanged.AddListener(delegate { ValueChangedSounds(); });

        // udate mixer values
        Mixer.SetFloat("MusicVolume", ValueToVolume(_slider0.value));
        Mixer.SetFloat("SoundsVolume", ValueToVolume(_slider1.value));
    }

    public void ValueChangedMusic()
    {
        Mixer.SetFloat("MusicVolume", ValueToVolume(_slider0.value));
        _saveFile.SetVolumes(_slider0.value, _slider1.value);
    }

    public void ValueChangedSounds()
    {
        Mixer.SetFloat("SoundsVolume", ValueToVolume(_slider1.value));
        _saveFile.SetVolumes(_slider0.value, _slider1.value);
    }

    public void RestartGame()
    {
        LevelsControl levels = new LevelsControl();
        levels.OpenedLevels = new List<string>(5);
        levels.CurrentLevel = "FirstLevel";
        File.WriteAllText("SaveData.json", JsonUtility.ToJson(levels));
        File.WriteAllText(GetComponent<LoadingPosition>().FileName + ".json",
                        JsonUtility.ToJson(new Vector2(-105, -20)));
    }

    public static float ValueToVolume(float value)
    {
        return (value * 40) - 35;
    }

    private void OnDestroy()
    {
        _saveFile.WriteToJson();
    }
}
