using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPanelManager : MonoBehaviour
{
    public GameObject KeyEast;
    public GameObject InsertKeyText;
    public GameObject SwitchOff;
    public GameObject SwitchOffKey;
    public GameObject KeyHoleKey;
    public GameObject Cable;

    // Start is called before the first frame update
    void Start()
    {
        KeyEast.SetActive(false);
        InsertKeyText.SetActive(false);
        KeyHoleKey.SetActive(false);
        Cable.SetActive(false);

        SwitchOff.SetActive(true);
        SwitchOffKey.SetActive(false);
    }

    private void OnTriggerExit(Collider other)
    {
        InsertKeyText.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            InsertKeyText.SetActive(true);

            if (Input.GetKey(KeyCode.E))
            {
                this.gameObject.SetActive(false);
                KeyEast.SetActive(true);
                SwitchOff.SetActive(false);
                SwitchOffKey.SetActive(true);
                KeyHoleKey.SetActive(true);
                InsertKeyText.SetActive(false);
            }
        }
    }
}