using UnityEngine;

/// <summary>
/// Subscribes to a GameEvent, and plays an audio clip through the referenced audiosource when notified. It is possible to make this play only once.
/// </summary>

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
