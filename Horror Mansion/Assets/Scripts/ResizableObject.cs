using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizableObject : MonoBehaviour
{
    [SerializeField] FloatList floatList;
    Vector3 defaultScale;
    int currentSize;
    int correctSize;
    public bool correct;

    void Start()
    {
        currentSize = floatList.GetFloatVarPosition("Default");
        correctSize = floatList.GetFloatVarPosition("Correct");
        defaultScale = transform.localScale;
    }

    public void Shrink()
    {
        if(currentSize > 0)
        {
            currentSize--;
            transform.localScale = defaultScale * floatList.floatVars[currentSize].value;
        }
        if (currentSize == correctSize)
        {
            correct = true;
        }
        else
        {
            correct = false;
        }
    }

    public void Enlarge()
    {
        if (currentSize < floatList.floatVars.Count - 1)
        {
            currentSize++;
            transform.localScale = defaultScale * floatList.floatVars[currentSize].value;
        }
        if (currentSize == correctSize)
        {
            correct = true;
        }
        else
        {
            correct = false;
        }
    }
}
