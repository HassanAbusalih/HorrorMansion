using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is attached to a trigger collider below the level. If an interactable were to somehow clip out of bounds and be dropped by
/// the player, this will reset the interactable back to its default position.
/// </summary>

public class ResetBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Interactable>(out Interactable interactable))
        {
            interactable.transform.position = interactable.defaultPos;
        }
    }
}
