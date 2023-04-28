using UnityEngine;

/// <summary>
/// Serializable class that holds data relating to a pick up interactable, and sets a public getter for it.
/// </summary>

[System.Serializable]
public class PickUpInteractable
{
    [Range(0, 50)][SerializeField] float throwForce;
    public float ThrowForce { get => throwForce; }
}
