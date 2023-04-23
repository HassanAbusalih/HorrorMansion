using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lockdown : MonoBehaviour, ISubscriber
{
    [SerializeField] GameEvent incoming;
    public GameEvent Subscriber => incoming;
    public string GetName() => nameof(incoming);
    [SerializeField] GameObject alarmLight;
    [SerializeField] GameObject normalLight;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip alarm;
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
                alarmLight.SetActive(!alarmLight.activeSelf);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!enabled) { return; }
        if (other.gameObject.TryGetComponent<PlayerController>(out var controller))
        {
            normalLight.SetActive(false);
            alarmLight.SetActive(true);
            active = true;
            source.clip = alarm;
            source.loop = true;
            source.Play();
        }
    }

    void Unlock()
    {
        normalLight.SetActive(true);
        alarmLight.SetActive(false);
        active = false;
        source.loop = false;
        source.Stop();
    }
}
