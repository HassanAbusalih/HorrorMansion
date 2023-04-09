using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [HideInInspector] public bool canInteract = true;
    [TextArea(5, 10)] public string description;
    public InteractType interactType = InteractType.None;
    public GameEvent gameEvent;
    Transform playerPos;
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
            if (distance <= 8)
            {
                myRenderer.materials = shaderMaterials;
                return;
            }
        }
        myRenderer.materials = defaultMaterials;
    }

    private void SetUpMaterialsAndShader()
    {
        myRenderer = GetComponent<Renderer>();
        defaultMaterials = myRenderer.materials;
        shaderMaterials = new Material[] { defaultMaterials[0], new Material(Shader.Find("Shader Graphs/Outline")) };
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