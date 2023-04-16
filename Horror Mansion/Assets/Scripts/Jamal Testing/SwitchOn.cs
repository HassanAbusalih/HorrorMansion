using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchOn : MonoBehaviour
{
    public GameObject SwitchO;
    public GameObject SwitchOff;
    public GameObject SwitchText;
    public GameObject Cable;

    // Start is called before the first frame update
    void Start()
    {
        SwitchO.SetActive(false);
        SwitchText.SetActive(false);
        Cable.SetActive(false);
    }

    private void OnTriggerExit(Collider other)
    {
        SwitchText.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SwitchText.SetActive(true);

            if (Input.GetKey(KeyCode.E))
            {
                SwitchO.SetActive(true);
                Cable.SetActive(true);
                SwitchOff.SetActive(false);
                SwitchText.SetActive(false);
            }
        }
    }
}