using TMPro;
using UnityEngine;

/// <summary>
/// This script is to be attached to the text prefab that is instantiated on text interactables. It gets its description, object to face and destroy time from the interactable that instantiates it.
/// It then uses those to face the player until it reaches the designated time, where it destroys the gameobject.
/// </summary>

public class FacePlayer : MonoBehaviour
{
    Transform objectToFace;
    TextMeshProUGUI textMeshProUGUI;
    string description;
    float destroyTime;
    float lifeTime;
    public string Description { set => description = value; }
    public Transform ObjectToFace { set => objectToFace = value; }
    public float DestroyTime { set => destroyTime = value; }

    private void Start()
    {
        textMeshProUGUI.text = description;
    }

    void Update()
    {
        lifeTime += Time.deltaTime;
        if (objectToFace != null )
        {
            transform.forward = objectToFace.forward;
        }
        if (destroyTime <= lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
