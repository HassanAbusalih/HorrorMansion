using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonScript : MonoBehaviour
{
    Transform playerPos;
    public int keypadNumber = 1;

    public UnityEvent KeypadClicked;

    private void Start()
    {
        playerPos = FindObjectOfType<PlayerController>().transform;
    }

    private void OnMouseDown()
    {
        float distance = (playerPos.position - transform.position).magnitude;
        if (distance < 3f)
        {
            KeypadClicked.Invoke();
        }
    }

}
