using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public bool canInteract = true;
    public InteractType interactType = InteractType.None;
    public Transform playerPos;
    Renderer myRenderer;
    Material[] defaultMaterials;
    Material[] shaderMaterials;
    Color[] defaultColors = new Color[3] {new Color(1, 1, 0.4f), new Color(0.4f, 0.9f, 0.4f), new Color(0.25f, 0.7f, 1)};
    [SerializeField] public TextInteractable text;
    [SerializeField] public ButtonInteractable button;
    [SerializeField] public PickUpInteractable pickUp;

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
        ToggleOutline();
    }

    private void ToggleOutline()
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

    public void ShowDescription()
    {
        GameObject desc = Instantiate(text.TextPrefab, new Vector3 (transform.position.x, transform.position.y + transform.localScale.y, transform.position.z), transform.rotation);
        desc.GetComponent<TextMeshPro>().text = text.Description;
        desc.GetComponent<FacePlayer>().objectToFace = playerPos;
        Destroy(desc, 5);
    }

    public void PushButton()
    {
        if (button.VariableNeeded)
        {
            if (button.ComponentNeeded)
            {
                button.GameEvent.NotifyObj(button.ComponentToPassIn);
            }
            else
            {
                button.GameEvent.NotifyObj(button.StringToPassIn);
            }
        }
        else
        {
            button.GameEvent.Notify();
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