using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Volume_Slider : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioMixer audioMixer;

    public AudioMixer MusicMixer;

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("SoundVolume", volume);
    }

    public void SetMusicVolume(float Mvolume)
    {
        MusicMixer.SetFloat("MusicVolume", Mvolume);
    }
}
