using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternateResizable : MonoBehaviour
{
    [SerializeField] FloatList floatList;
    float currentSize;
    Vector3 defaultScale;
    float maxSize;
    float minSize;
    float correctMinSize;
    float correctMaxSize;
    public bool correct;

    void Start()
    {
        maxSize = floatList.GetFloatVar("Max").value;
        minSize = floatList.GetFloatVar("Min").value;
        currentSize = floatList.GetFloatVar("Default").value;
        correctMinSize = floatList.GetFloatVar("Min Correct").value;
        correctMaxSize = floatList.GetFloatVar("Max Correct").value;
        defaultScale = transform.localScale;
    }

    public void Shrink(float resizeSpeed)
    {
        if (currentSize > minSize)
        {
            currentSize -= resizeSpeed * Time.deltaTime;
            transform.localScale = defaultScale * currentSize;
        }
        if (currentSize >= correctMinSize && currentSize <= correctMaxSize)
        {
            correct = true;
        }
        else
        {
            correct = false;
        }
    }

    public void Enlarge(float resizeSpeed)
    {
        if (currentSize < maxSize)
        {
            currentSize += resizeSpeed * Time.deltaTime;
            transform.localScale = defaultScale * currentSize;
        }
        if (currentSize >= correctMinSize && currentSize <= correctMaxSize)
        {
            correct = true;
        }
        else
        {
            correct = false;
        }
    }
}
