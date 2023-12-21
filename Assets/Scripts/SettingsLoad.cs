using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsLoad : MonoBehaviour
{
    public AudioMixer Mixer;
    public string SaveFileName;

    void Start()
    {
        SettingsFile settings = new SettingsFile(SaveFileName);

        Mixer.SetFloat("MusicVolume", Settings.ValueToVolume(settings.MusicVolume));
        Mixer.SetFloat("SoundsVolume", Settings.ValueToVolume(settings.SoundsVolume));
    }
}
