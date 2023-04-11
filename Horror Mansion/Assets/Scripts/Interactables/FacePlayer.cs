using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    public Transform objectToFace;

    void Update()
    {
        transform.forward = objectToFace.forward;
    }
}
