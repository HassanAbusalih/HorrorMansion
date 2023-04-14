using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InstantResizable : Resizable
{
    [SerializeField] FloatList floatList;
    [SerializeField] bool allowPickUpWhenCorrect;
    UnityEvent correctSize = new();
    Vector3 defaultScale;
    int correctObjectSize;
    bool correct;

    void Start()
    {
        currentSize = floatList.GetFloatVarPosition("Default");
        correctObjectSize = floatList.GetFloatVarPosition("Correct");
        defaultScale = transform.localScale;
        if (allowPickUpWhenCorrect)
        {
            Interactable interactable = GetComponent<Interactable>();
            if (interactable == null)
            {
                interactable = gameObject.AddComponent<Interactable>();
            }
            if (currentSize == correctObjectSize)
            {
                correct = true;
            }
            interactable.SetEvent(correctSize, correct);
        }
    }

    public void Shrink()
    {
        if(currentSize > 0)
        {
            currentSize--;
            transform.localScale = defaultScale * floatList.floatVars[(int)currentSize].value;
        }
        IsCorrect();
    }

    public void Enlarge()
    {
        if (currentSize < floatList.floatVars.Count - 1)
        {
            currentSize++;
            transform.localScale = defaultScale * floatList.floatVars[(int)currentSize].value;
        }
        IsCorrect();
    }

    void IsCorrect()
    {
        if (currentSize == correctObjectSize && !correct)
        {
            correct = true;
            correctSize?.Invoke();
        }
        else if (currentSize != correctObjectSize && correct)
        {
            correct = false;
            correctSize?.Invoke();
        }
    }
}

public class Resizable : MonoBehaviour
{
    [HideInInspector] public float currentSize = 1;
}