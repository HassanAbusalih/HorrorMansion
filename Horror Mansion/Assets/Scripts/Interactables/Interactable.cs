using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Contains all the data an interactable might need (which is set in the inspector), as well as (almost) all methods relating to interactables.
/// It also handles adding/removing a shader graph to give the interactable an outline based on player proximity.
/// </summary>

public class Interactable : MonoBehaviour, INotifier
{
    public bool canInteract = true;
    public InteractType interactType = InteractType.None;
    public Transform playerPos;
    Renderer myRenderer;
    Material[] defaultMaterials;
    Material[] shaderMaterials;
    [SerializeField] GameEvent gameEvent;
    Color[] defaultColors = new Color[3] {new Color(1, 1, 0.4f), new Color(0.4f, 0.9f, 0.4f), new Color(0.25f, 0.7f, 1)};
    [SerializeField] public TextInteractable text;
    [SerializeField] public ButtonInteractable button;
    [SerializeField] public PickUpInteractable pickUp;
    GameObject descriptionText;
    public Vector3 defaultPos;
    public GameEvent Notifier { get => gameEvent; }

    private void Start()
    {
        if (interactType == InteractType.PickUp && GetComponent<Rigidbody>() == null)
        {
            gameObject.AddComponent<Rigidbody>();
        }
        defaultPos = transform.position;
        playerPos = FindFirstObjectByType<FirstPersonCam>().transform;
        SetUpMaterialsAndShader();
    }

    private void Update()
    {
        ToggleOutline();
    }

    private void ToggleOutline()
    {
        if (myRenderer == null) { return; }
        if (canInteract)
        {
            float distance = (playerPos.position - transform.position).magnitude;
            if (distance <= 3.3)
            {
                myRenderer.materials = shaderMaterials;
                if (interactType != InteractType.None)
                {
                    shaderMaterials[shaderMaterials.Length - 1].color = defaultColors[(int)interactType - 1];
                }
                return;
            }
        }
        myRenderer.materials = defaultMaterials;
    }
    private void SetUpMaterialsAndShader()
    {
        TryGetComponent(out myRenderer);
        if (myRenderer == null) { return; }
        defaultMaterials = myRenderer.materials;
        shaderMaterials = new Material[defaultMaterials.Length + 1];
        for(int i = 0; i < shaderMaterials.Length - 1; i++)
        {
            shaderMaterials[i] = defaultMaterials[i];
        }
        shaderMaterials[shaderMaterials.Length - 1] = new Material(Shader.Find("Shader Graphs/Outline"));
    }

    /// <summary>
    /// Instantiates a prefab of the text gameobject on the object (and adds it's box collider's size.y to make it spawn on top). It also gets the FacePlayer component on the prefab,
    /// sets the player to be the object to face, gives it the description set in the interactable's inspector, as well the the time to destroy the instantiated prefab.
    /// </summary>
    public void ShowDescription()
    {
        if (descriptionText == null)
        {
            if (TryGetComponent<BoxCollider>(out var collider))
            {
                descriptionText = Instantiate(text.TextPrefab, new Vector3(transform.position.x, transform.position.y + collider.size.y, transform.position.z), transform.rotation);
            }
            else
            {
                Debug.Log($"Make {gameObject.name} a box collider!");
            }
            FacePlayer facePlayer = descriptionText.GetComponent<FacePlayer>();
            facePlayer.ObjectToFace = playerPos;
            facePlayer.Description = text.Description;
            facePlayer.DestroyTime = text.DestroyTime;
        }
    }

    /// <summary>
    /// Based on the settings set in the inspector, this decides which GameEvent method to notify (and which variable, if any, to pass in). If the button is set to be animated,
    /// it will play the animation, and if this is set to be a single interaction, it will destroy itself afterwards.
    /// </summary>
    public void PushButton()
    {
        if (button.VariableNeeded)
        {
            if (button.ComponentNeeded)
            {
                gameEvent.NotifyObj(button.ComponentToPassIn);
            }
            else
            {
                gameEvent.NotifyObj(button.StringToPassIn);
            }
        }
        else
        {
            gameEvent.Notify();
        }
        if (button.Animated)
        {
            button.Animator.Play(button.AnimationName);
        }
        if (button.SingleInteraction)
        {
            canInteract = false;
            Destroy(this, 0.1f);
        }
    }

    /// <summary>
    /// This method is for establishing a link between the resizable and interactable components on an object, to only allow the player to pick up the object at the correct size.
    /// </summary>
    /// <param name="toggleEvent"> The Unity Event in the resizable script that the interactable will add a listener to.</param>
    /// <param name="correct"> The current state of the interactable, to align the two scripts together. </param>
    public void SetEvent(UnityEvent toggleEvent, bool correct)
    {
        toggleEvent.AddListener(ToggleState);
        interactType = InteractType.PickUp;
        canInteract = correct;
    }

    void ToggleState()
    {
        canInteract = !canInteract;
    }

    public string GetName()
    {
        return nameof(gameEvent);
    }
}

public enum InteractType
{
    None,
    PickUp,
    Text,
    Button
}