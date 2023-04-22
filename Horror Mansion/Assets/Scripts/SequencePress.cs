using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequencePress : MonoBehaviour
{
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;
    public GameObject resizeObjectPanel;
    // Start is called before the first frame update
    void Start()
    {
        resizeObjectPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject == button1)
        {
            if (Input.GetKey(KeyCode.E))
            {
                button2.GetComponent<Collider>();
            }
        }
        if (col.gameObject == button2)
        {
            if (Input.GetKey(KeyCode.E))
            {
                button3.GetComponent<Collider>();
            }
        }
        if (col.gameObject == button3)
        {
            if (Input.GetKey(KeyCode.E))
            {
                button4.GetComponent<Collider>();
            }
        }
        if (col.gameObject == button4)
        {
            if (Input.GetKey(KeyCode.E))
            {
                resizeObjectPanel.SetActive(false);
            }
        }

    }
}
