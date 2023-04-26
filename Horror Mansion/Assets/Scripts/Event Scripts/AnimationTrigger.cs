using UnityEngine;

/// <summary>
/// Subscribes to a GameEvent, and triggers an animation when that GameEvent is notified.
/// </summary>
public class AnimationTrigger : MonoBehaviour, ISubscriber
{
    [SerializeField] GameEvent incoming;
    [SerializeField] Animator animator;
    [SerializeField] string animationName;
    public GameEvent Subscriber => incoming;

    public string GetName() => nameof(incoming);

    void Start()
    {
        incoming.Subscribe(PlayAnimation);
    }

   void PlayAnimation()
    {
        animator.Play(animationName);
    }
}
