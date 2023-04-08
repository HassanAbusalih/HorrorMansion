using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizePuzzle : MonoBehaviour
{
    public GameObject miniDoor;
    // Start is called before the first frame update
    void Start()
    {
        miniDoor.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Shape")
        {
            miniDoor.SetActive(false);
        }
    }
}
