using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [HideInInspector] public bool canInteract = true;
    [TextArea(5, 10)] public string description;
    [Range(0, 50)] public float throwForce;
    public bool variableNeeded;
    [Tooltip("This will pass a string through the GameEvent.")]
    public string stringToPassIn;
    public GameObject textPrefab;
    public bool singleInteraction;
    public bool animated;
    public Animator animator;
    public string animationName;
    public InteractType interactType = InteractType.None;
    Color[] defaultColors = new Color[3] {new Color(1, 1, 0.4f), new Color(0.4f, 0.9f, 0.4f), new Color(0.25f, 0.7f, 1)};
    public GameEvent gameEvent;
    public Transform playerPos;
    Renderer myRenderer;
    Material[] defaultMaterials;
    Material[] shaderMaterials;

    private void Start()
    {
        if (interactType == InteractType.PickUp && GetComponent<Rigidbody>() == null)
        {
            gameObject.AddComponent<Rigidbody>();
        }
        playerPos = FindFirstObjectByType<FirstPersonCam>().transform;
        SetUpMaterialsAndShader();
    }

    private void Update()
    {
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

    public void ShowDescription()
    {
        GameObject desc = Instantiate(textPrefab, new Vector3 (transform.position.x, transform.position.y + transform.localScale.y, transform.position.z), transform.rotation);
        desc.GetComponent<TextMeshPro>().text = description;
        desc.GetComponent<FacePlayer>().objectToFace = playerPos;
        Destroy(desc, 5);
    }

    public void PushButton()
    {
        if (variableNeeded)
        {
            gameEvent.NotifyObj(stringToPassIn);
        }
        else
        {
            gameEvent.Notify();
        }
        if (animated)
        {
            animator.Play(animationName);
        }
        if (singleInteraction)
        {
            canInteract = false;
            Destroy(this, 0.1f);
        }
    }
    private void SetUpMaterialsAndShader()
    {
        myRenderer = GetComponent<Renderer>();
        defaultMaterials = myRenderer.materials;
        shaderMaterials = new Material[defaultMaterials.Length + 1];
        for(int i = 0; i < shaderMaterials.Length - 1; i++)
        {
            shaderMaterials[i] = defaultMaterials[i];
        }
        shaderMaterials[shaderMaterials.Length - 1] = new Material(Shader.Find("Shader Graphs/Outline"));
    }

    public void SetEvent(UnityEvent gameEvent, bool correct)
    {
        gameEvent.AddListener(ToggleState);
        interactType = InteractType.PickUp;
        canInteract = correct;
    }

    void ToggleState()
    {
        canInteract = !canInteract;
    }
}

public enum InteractType
{
    None,
    PickUp,
    Text,
    Button
}