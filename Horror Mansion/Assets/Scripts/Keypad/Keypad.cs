using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Keypad : MonoBehaviour, INotifier
{
    [SerializeField] GameEvent outgoing;
    public GameEvent Notifier => outgoing;
    public string GetName() => nameof(outgoing);
    [SerializeField] TextMeshPro codeText;
    public string password = "1234";
    private string userInput;
    private bool check;
    public AudioClip clickSound;
    public AudioClip openSound;
    public AudioClip wrongSound;
    AudioSource audioSource;
    bool solved;

    private void Start()
    {
        ResetUserInput();
        audioSource = GetComponent<AudioSource>();
    }


    public void ButtonClicked(string number)
    {
        audioSource.PlayOneShot(clickSound);
        AddNumber(number);
        if (check)
        {
            check = false;
            //check password
            if (userInput == password)
            {
                // TODO Invoke event and play sound
                //Debug.Log("Entry Allowed");
                ResetUserInput();
                if (!solved)
                {
                    audioSource.PlayOneShot(openSound);
                    outgoing.Notify();
                    solved = true;
                }
            }
            else
            {
                //TODO play a sound
                //Debug.Log("Not this time");
                ResetUserInput();
                audioSource.PlayOneShot(wrongSound);
            }
        }
    }

    private void AddNumber(string number)
    {
        for (int i = 0; i < userInput.Length; i++)
        {
            if (userInput[i] == '-')
            {
                if (i < userInput.Length - 1)
                {
                    userInput = userInput.Substring(0, i) + number + userInput.Substring(i + 1);
                }
                else
                {
                    userInput = userInput.Substring(0, i) + number;
                    check = true;
                }
                break;
            }
        }
        codeText.text = userInput;
    }

    void ResetUserInput()
    {
        userInput = "";
        for (int i = 0; i < password.Length; i++)
        {
            userInput += "-";
        }
        codeText.text = userInput;
    }

}
