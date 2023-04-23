using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Keypad : MonoBehaviour, INotifier
{
    [SerializeField] GameEvent outgoing;
    public GameEvent Notifier => outgoing;
    public string GetName() => nameof(outgoing);
    public string password = "1234";
    private string userInput = "";
    public AudioClip clickSound;
    public AudioClip openSound;
    public AudioClip wrongSound;
    AudioSource audioSource;
    private int num;

    private void Start()
    {
        userInput = "";
        audioSource = GetComponent<AudioSource>();
        num = password.Length;
    }


    public void ButtonClicked(string number)
    {
        audioSource.PlayOneShot(clickSound);
        userInput += number;
        if (userInput.Length >= num)
        {
            //check password
            if (userInput == password)
            {
                // TODO Invoke event and play sound
                Debug.Log("Entry Allowed");
                audioSource.PlayOneShot(openSound);
                userInput = "";
                outgoing.Notify();
            }
            else
            {
                //TODO play a sound
                Debug.Log("Not this time");
                userInput = "";
                audioSource.PlayOneShot(wrongSound);
            }
        }
    }

}
