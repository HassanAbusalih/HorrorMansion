using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    Transform objectToFace;
    string description;
    public TextMeshProUGUI textMeshProUGUI;
    [SerializeField] float destroyTime;
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
