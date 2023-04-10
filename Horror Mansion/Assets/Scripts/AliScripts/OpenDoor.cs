using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OpenDoor : MonoBehaviour
{
    AudioSource doorAudioSource;
    public AudioClip doorOpen;
    public AudioClip wrongCode;
    public AudioClip buttonClick;
    //public AudioSource WrongCode;


    private Animator anim;

    private bool IsAtDoor = false;

   [SerializeField] private TextMeshProUGUI CodeText;
    string codeTextValue = "";
    public string safeCode;
    public GameObject CodePanel;
    bool complete;
    FirstPersonCam firstPersonCam;
    // public GameObject CameraRotation;


    void Start()
    {
        anim = GetComponent<Animator>();
        doorAudioSource = GetComponent<AudioSource>();
        firstPersonCam = FindObjectOfType<FirstPersonCam>();
    }

    public void play_sound()
    {
        //Using this function to play sound with animation
        /// set the audio clip to be the open door clip
        doorAudioSource.clip = doorOpen;
        doorAudioSource.Play();
    }
   
    void Update()
    {
        CodeText.text = codeTextValue;

        if(codeTextValue == safeCode && !complete)
        {
            complete = true;
            anim.SetTrigger("OpenDoor");
            doorAudioSource.PlayOneShot(doorOpen);
            CodePanel.SetActive(false);
        }

        if(codeTextValue.Length >= 4)
        {
            codeTextValue = "";
            // set the audio clip to b wrong code
            doorAudioSource.PlayOneShot(wrongCode);
        }

        if (Input.GetKey(KeyCode.E) && IsAtDoor == true)
        {
            CodePanel.SetActive(true);
            firstPersonCam.enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;

            
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
        firstPersonCam.enabled = true;
        CodePanel.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void AddDigit(string digit)
    {
        doorAudioSource.PlayOneShot(buttonClick);
        codeTextValue += digit;
    }
}
