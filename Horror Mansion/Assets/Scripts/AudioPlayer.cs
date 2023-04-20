using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour, ISubscriber
{
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip clip;
    [SerializeField] GameEvent incoming;
    public GameEvent Subscriber => incoming;
    string ISubscriber.GetName() => nameof(incoming);
    [SerializeField] bool singleInteraction;

    private void Start()
    {
        incoming.Subscribe(PlayAudio);
    }

    private void PlayAudio()
    {
        source.clip = clip;
        if (singleInteraction)
        {
            source.Play();
            incoming.Unsubscribe(PlayAudio);
        }
        else if (!source.isPlaying)
        {
            source.Play();
        }
    }
}
