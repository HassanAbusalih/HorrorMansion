using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoTrig : MonoBehaviour
{
    public Text forKeypad;
    public Image forControls;
    // Start is called before the first frame update
    void Start()
    {
        forKeypad.enabled = false;
        //forControls.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(forKeypad.enabled && Input.GetKey(KeyCode.Return))
        {
            forKeypad.enabled = false;
            Destroy(this);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            forKeypad.enabled = true;
        }
        

    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(forKeypad);
        }
    }
}

