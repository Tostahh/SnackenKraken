using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundGA : GameAction
{
    [SerializeField] private AudioSource AudioSource;
    public override void Action()
    {
        if(AudioSource == null)
        {
            AudioSource = FindObjectOfType<SeaCritterController>().GetComponent<AudioSource>();
        }

        AudioSource.Play();
    }
}
