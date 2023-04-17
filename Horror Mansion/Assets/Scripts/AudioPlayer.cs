using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour, ISubscriber
{
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip clip;
    [SerializeField] GameEvent incoming;
    string ISubscriber.GetName() => nameof(incoming);
    [SerializeField] bool singleInteraction;

    private void Start()
    {
        incoming.Subscribe(PlayAudio);
    }

    private void PlayAudio()
    {
        if (singleInteraction)
        {
            source.PlayOneShot(clip);
            incoming.Unsubscribe(PlayAudio);
        }
        else
        {
            source.PlayOneShot(clip);
        }
    }
}
