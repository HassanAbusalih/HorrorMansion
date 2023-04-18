using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EastWingUSB1 : MonoBehaviour
{
    public GameObject EastUSB1;
    public GameObject PickUpText;
    public GameObject Keyhole;

    // Start is called before the first frame update
    void Start()
    {
        EastUSB1.SetActive(false);
        PickUpText.SetActive(false);
        Keyhole.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PickUpText.SetActive(true);

            if (Input.GetKey(KeyCode.E))
            {
                this.gameObject.SetActive(false);
                EastUSB1.SetActive(true);

                PickUpText.SetActive(false);
                Keyhole.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PickUpText.SetActive(false);
    }
}