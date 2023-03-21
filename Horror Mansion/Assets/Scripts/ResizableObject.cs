using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResizableObject : MonoBehaviour
{
    [SerializeField] FloatList floatList;
    [SerializeField] bool allowPickUpWhenCorrect;
    UnityEvent correctSize = new();
    Vector3 defaultScale;
    int currentSize;
    int correctObjectSize;
    bool correct;

    void Start()
    {
        currentSize = floatList.GetFloatVarPosition("Default");
        correctObjectSize = floatList.GetFloatVarPosition("Correct");
        defaultScale = transform.localScale;
        if (allowPickUpWhenCorrect)
        {
            PickUpable pickUpable = GetComponent<PickUpable>();
            if (pickUpable == null)
            {
                pickUpable = gameObject.AddComponent<PickUpable>();
            }
            pickUpable.SetEvent(correctSize, floatList);
        }
    }

    public void Shrink()
    {
        if(currentSize > 0)
        {
            currentSize--;
            transform.localScale = defaultScale * floatList.floatVars[currentSize].value;
        }
        IsCorrect();
    }

    public void Enlarge()
    {
        if (currentSize < floatList.floatVars.Count - 1)
        {
            currentSize++;
            transform.localScale = defaultScale * floatList.floatVars[currentSize].value;
        }
        IsCorrect();
    }

    void IsCorrect()
    {
        if (currentSize == correctObjectSize && !correct)
        {
            correct = true;
            correctSize.Invoke();
        }
        else if (currentSize != correctObjectSize && correct)
        {
            correct = false;
            correctSize.Invoke();
        }
    }
}
