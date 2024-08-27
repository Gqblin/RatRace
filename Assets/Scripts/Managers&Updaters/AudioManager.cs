using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] AudioMixerGroup masterGroup;
    [SerializeField] AudioMixerGroup musicGroup;
    public AudioMixerGroup baseGroup;
    public AudioMixerGroup dynamicGroup;
    public AudioMixerGroup sfxGroup;

    private void Awake()
    {
        if (this != instance && instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
