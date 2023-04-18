using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Keypad : MonoBehaviour
{
    public string password = "1234";

    private string userInput = "";

    public AudioClip clickSound;
    public AudioClip openSound;
    public AudioClip noSound;
    AudioSource audioSource;

    public UnityEvent OnEntryAllowed;


    private void Start()
    {
        userInput = "";
        audioSource = GetComponent<AudioSource>();
    }


    public void ButtonClicked(string number)
    {
        audioSource.PlayOneShot(clickSound);
        userInput += number;
        if (userInput.Length >= 4)
        {
            //check password
            if (userInput == password)
            {
                // TODO Invoke event and play sound
                Debug.Log("Entry Allowed");
                audioSource.PlayOneShot(openSound);
                OnEntryAllowed.Invoke();
            }
            else
            {
                //TODO play a sound
                Debug.Log("Not this time");
                userInput = "";
                audioSource.PlayOneShot(noSound);
            }
        }
    }
}
