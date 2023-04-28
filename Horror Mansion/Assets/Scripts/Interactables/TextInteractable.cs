using UnityEngine;

/// <summary>
/// Serializable class that holds all data relating to a text interactable, and sets public getters for all of them.
/// </summary>

[System.Serializable]
public class TextInteractable
{
    [SerializeField] GameObject textPrefab;
    [TextArea(5, 10)][SerializeField] string description;
    [SerializeField] float destroyTime = 5f;
    public GameObject TextPrefab { get => textPrefab; }
    public string Description { get => description; }
    public float DestroyTime { get => destroyTime; }
}
