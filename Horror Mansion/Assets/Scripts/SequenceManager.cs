using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceManager : MonoBehaviour
{
    [SerializeField] GameEvent incoming;
    [SerializeField] GameEvent outgoing;
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
        int num = int.Parse((string)number);
        if (num == counter + 1)
        {
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
