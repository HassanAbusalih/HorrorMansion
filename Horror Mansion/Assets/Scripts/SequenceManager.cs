using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceManager : MonoBehaviour, INotifier, ISubscriber
{
    [SerializeField] GameEvent incoming;
    [SerializeField] GameEvent outgoing;
    string ISubscriber.GetName() => nameof(incoming);
    string INotifier.GetName() => nameof(outgoing);
    [SerializeField] int numberOfButtons;
    [SerializeField] Animator animator;
    [SerializeField] string animationName;
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
        }
        else
        {
            counter = 0;
        }
        if (counter == numberOfButtons && !puzzleSolved)
        {
            puzzleSolved = true;
            if (animator != null)
            {
                animator.Play(animationName);
            }
            outgoing.Notify();
            incoming.UnsubscribeObj(DoSequence);
        }
    }
}
