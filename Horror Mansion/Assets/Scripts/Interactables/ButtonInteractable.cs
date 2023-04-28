using UnityEngine;

/// <summary>
/// Serializable class that holds all data relating to a button interactable, and sets public getters for all of them.
/// </summary>

[System.Serializable]
public class ButtonInteractable
{
    [SerializeField] bool variableNeeded;
    [SerializeField] bool componentNeeded;
    [SerializeField] Component componentToPassIn;
    [SerializeField] string stringToPassIn;
    [SerializeField] bool singleInteraction;
    [SerializeField] bool animated;
    [SerializeField] Animator animator;
    [SerializeField] string animationName;
    public bool VariableNeeded { get => variableNeeded; }
    public bool ComponentNeeded { get => componentNeeded; }
    public Component ComponentToPassIn { get => componentToPassIn; }
    public string StringToPassIn { get => stringToPassIn; }
    public bool SingleInteraction { get => singleInteraction; }
    public bool Animated { get => animated; }
    public Animator Animator { get => animator; }
    public string AnimationName { get => animationName; }
}
