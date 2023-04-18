using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryUiDeactivate : MonoBehaviour
{
    public GameObject Deactivate;
    public GameObject Activate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Deactivate.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Activate.SetActive(true);
        }
    }
}
