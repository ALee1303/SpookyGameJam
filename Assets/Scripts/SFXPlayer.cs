using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : Singleton<SFXPlayer> 
{
    [SerializeField]
    private AudioSource efxSource;                   //Drag a reference to the audio source which will play the sound effects.

    [SerializeField]
    private float lowPitch = 0.5f;
    [SerializeField]
    private float highPitch = 1.5f;


    //Used to play single sound clips.
    public void PlayClip(AudioClip clip, float vel)
    {
        //Set the clip of our efxSource audio source to the clip passed in as a parameter.
        efxSource.clip = clip;

        //Play the clip.
        efxSource.Play();
    }
}
