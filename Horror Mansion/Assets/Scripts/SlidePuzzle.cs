using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidePuzzle : MonoBehaviour
{
    public GameObject SlidePuz;
    public GameObject SolvePuzzleText;

    // Start is called before the first frame update
    void Start()
    {
        SolvePuzzleText.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            SolvePuzzleText.SetActive(true);

            if(Input.GetKey(KeyCode.E))
            {
                SlidePuz.SetActive(false);
                SolvePuzzleText.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        SolvePuzzleText.SetActive(false);
    }
}