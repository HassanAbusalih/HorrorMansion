using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Uses a Unity Event to pass in a specified number as a parameter to the Keypad script's ButtonClicked method. This happens when the player presses the mouse button
/// and is within a certain range.
/// </summary>

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
