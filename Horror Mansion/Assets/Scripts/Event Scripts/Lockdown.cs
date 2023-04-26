using UnityEngine;

/// <summary>
/// When the player walks into the trigger, this script disables all normal lights and enables alarm lights, as well as plays the alarm on a loop. It then alternates the active state of the 
/// alarm lights based on an interval that is set in the inspector.
/// When the GameEvent it is subscribed to is notified, it stops the alarm sound, disables alarm lights and turn the normal lights back on.
/// </summary>

public class Lockdown : MonoBehaviour, ISubscriber
{
    [SerializeField] GameEvent incoming;
    public GameEvent Subscriber => incoming;
    public string GetName() => nameof(incoming);
    [SerializeField] GameObject[] alarmLight;
    [SerializeField] GameObject[] normalLight;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip alarm;
    bool turnOn;
    bool active;
    float currentTime;
    [SerializeField] float flashInterval;

    void Start()
    {
        incoming.Subscribe(Unlock);
    }

    void Update()
    {
        if (active)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= flashInterval)
            {
                currentTime = 0;
                for (int i = 0; i < alarmLight.Length; i++)
                {
                    alarmLight[i].SetActive(!alarmLight[i].activeSelf);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!enabled) { return; }
        if (other.gameObject.TryGetComponent<PlayerController>(out var controller) && !turnOn)
        {
            turnOn = true;
            for (int i = 0; i < normalLight.Length; i++)
            {
                normalLight[i].SetActive(false);
            }
            for (int i = 0; i < alarmLight.Length; i++)
            {
                alarmLight[i].SetActive(true);
            }
            active = true;
            source.clip = alarm;
            source.loop = true;
            source.Play();
        }
    }

    void Unlock()
    {
        for (int i = 0; i < normalLight.Length; i++)
        {
            normalLight[i].SetActive(true);
        }
        for (int i = 0; i < alarmLight.Length; i++)
        {
            alarmLight[i].SetActive(false);
        }
        active = false;
        source.loop = false;
        source.Stop();
    }
}
