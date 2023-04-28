using UnityEngine;

/// <summary>
/// Determines if the player has pressed the correct sequence of buttons by checking the number passed in by the GameEvent it is subscribed to every time it is notified.
/// The expected input is 1, 2, 3, etc. A counter is incremented every time an expected value is received, and once the counter reaches the number of buttons set, the puzzle is marked
/// as solved and the outgoing GameEvent is notified.
/// If an incorrect number is passed in, the sequence is reset back to 0.
/// </summary>

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
        if (int.TryParse(number.ToString(), out int num) && num == counter + 1)
        {
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
