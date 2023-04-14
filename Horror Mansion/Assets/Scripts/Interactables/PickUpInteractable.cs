using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PickUpInteractable
{
    [Range(0, 50)][SerializeField] float throwForce;
    public float ThrowForce { get => throwForce; }
}
