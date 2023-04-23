using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour, ISubscriber
{
    [SerializeField] GameEvent incoming;
    [SerializeField] Animator animator;
    [SerializeField] string animationName;
    public GameEvent Subscriber => incoming;

    public string GetName()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        incoming.Subscribe(PlayAnimation);
    }

    // Update is called once per frame
   void PlayAnimation()
    {
        animator.Play(animationName);
    }
}
