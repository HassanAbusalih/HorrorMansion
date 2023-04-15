using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPuzzleButtonTemp : MonoBehaviour
{
    public GameObject ColorPuzzleButton;
    public GameObject PressText;
    public GameObject Keypad;
    public GameObject FakeDoor;
    public GameObject Cable;

    // Start is called before the first frame update
    void Start()
    {
        PressText.SetActive(false);
        Keypad.SetActive(false);    // Door Parent Deactivated
        Cable.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            {
                PressText.SetActive(true);

                if (Input.GetKey(KeyCode.E))
                {
                    this.gameObject.SetActive(false);
                    PressText.SetActive(false);

                    Keypad.SetActive(true); // Door Parent Active
                    FakeDoor.SetActive(false);
                    Cable.SetActive(true);
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        PressText.SetActive(false);
    }
}