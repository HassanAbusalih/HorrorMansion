using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpKeypad : MonoBehaviour
{
    public GameObject KeypadOffText;
  //  public GameObject CheckKeypad;

    // Start is called before the first frame update
    void Start()
    {
        KeypadOffText.SetActive(false);
    //    CheckKeypad.SetActive(false);
    }

    private void OnTriggerExit(Collider other)
    {
        KeypadOffText.SetActive(false);
      //  CheckKeypad.SetActive(false);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
 //           CheckKeypad.SetActive(true);

            if (Input.GetKey(KeyCode.E))
            {
                KeypadOffText.SetActive(true);
//                CheckKeypad.SetActive(false);
            }
        }
    }
}