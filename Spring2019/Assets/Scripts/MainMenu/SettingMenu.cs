using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
public class SettingMenu : MonoBehaviour {

    public AudioMixer audioMixer;

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }
}
