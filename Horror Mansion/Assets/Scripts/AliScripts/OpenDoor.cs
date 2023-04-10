using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OpenDoor : MonoBehaviour
{
    AudioSource DoorOpen;
    public AudioSource WrongCode;

    private Animator anim;

    private bool IsAtDoor = false;

   [SerializeField] private TextMeshProUGUI CodeText;
    string codeTextValue = "";
    public string safeCode;
    public GameObject CodePanel;
    // public GameObject CameraRotation;


    void Start()
    {
        anim = GetComponent<Animator>();
        DoorOpen = GetComponent<AudioSource>();
        WrongCode = GetComponent<AudioSource>();
    }

    public void play_sound()
    {
        //Using this function to play sound with animation
        DoorOpen.Play();
    }
   
    void Update()
    {
        CodeText.text = codeTextValue;

        if(codeTextValue == safeCode)
        {
            anim.SetTrigger("OpenDoor");
            CodePanel.SetActive(false);
        }

        if(codeTextValue.Length >= 4)
        {
            codeTextValue = "";
            WrongCode.Play(); //not working
        }

        if (Input.GetKey(KeyCode.E) && IsAtDoor == true)
        {
            CodePanel.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            IsAtDoor = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IsAtDoor = false;
        CodePanel.SetActive(false);
    }

    public void AddDigit(string digit)
    {
        codeTextValue += digit;
    }
}
