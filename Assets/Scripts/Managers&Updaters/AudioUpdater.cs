using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioUpdater : MonoBehaviour
{
    public AudioSource baseAudio;
    public AudioSource dynaAudio;
    [SerializeField] AudioClip[] selectedBGM;
    [SerializeField] bool containsDynamicAudio;
    [SerializeField] int baseMusic;
    [SerializeField] int dynamicMusic;

    private void Start()
    {
        SetAudioClips();
    }

    private void Update()
    {
        if (GameManager.instance.countDownInt == 0 && !baseAudio.isPlaying) { baseAudio.Play(); }
    }

    public void SetAudioClips()
    {
        if (selectedBGM != null)
        {
            baseAudio.clip = selectedBGM[baseMusic];
            if (containsDynamicAudio && selectedBGM[dynamicMusic] != null) 
            { dynaAudio.clip = selectedBGM[dynamicMusic]; }
        }
    }

    public void AdjustVolume(int desiredVolume)
    {

    }
}
