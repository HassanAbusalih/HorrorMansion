using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceManager : MonoBehaviour, INotifier, ISubscriber
{
    [SerializeField] GameEvent incoming;
    [SerializeField] GameEvent outgoing;
    public GameEvent Subscriber => incoming;
    public GameEvent Notifier => outgoing;
    string ISubscriber.GetName() => nameof(incoming);
    string INotifier.GetName() => nameof(outgoing);
    [SerializeField] int numberOfButtons;
    [SerializeField] Animator animator;
    [SerializeField] string animationName;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip correctSequence;
    [SerializeField] AudioClip wrongSequence;
    [SerializeField] AudioClip buttonPress;
    int counter;
    bool puzzleSolved;

    private void Start()
    {
        incoming.SubscribeObj(DoSequence);
    }

    void DoSequence(object number)
    {
        int num;
        if (int.TryParse(number.ToString(), out num) && num == counter + 1)
        {
            Debug.Log(num);
            counter++;
            audioSource.PlayOneShot(buttonPress);
        }
        else
        {
            counter = 0;
            audioSource.PlayOneShot(wrongSequence);
        }
        if (counter == numberOfButtons && !puzzleSolved)
        {
            puzzleSolved = true;
            if (animator != null)
            {
                animator.Play(animationName);
            }
            outgoing.Notify();
            audioSource.PlayOneShot(correctSequence);
            incoming.UnsubscribeObj(DoSequence);
        }
    }
}
