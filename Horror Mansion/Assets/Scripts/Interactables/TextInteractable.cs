using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TextInteractable
{
    [SerializeField] GameObject textPrefab;
    [TextArea(5, 10)][SerializeField] string description;
    public GameObject TextPrefab { get => textPrefab; }
    public string Description { get => description; }
}
