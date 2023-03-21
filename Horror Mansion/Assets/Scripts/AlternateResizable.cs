using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AlternateResizable : MonoBehaviour
{
    [SerializeField] FloatList floatList;
    [SerializeField] bool allowPickUpWhenCorrect;
    UnityEvent correctSize = new();
    Vector3 defaultScale;
    bool correct;
    float currentSize;
    float maxSize;
    float minSize;
    float correctMinSize;
    float correctMaxSize;

    void Start()
    {
        maxSize = floatList.GetFloatVar("Max").value;
        minSize = floatList.GetFloatVar("Min").value;
        currentSize = floatList.GetFloatVar("Default").value;
        correctMinSize = floatList.GetFloatVar("Min Correct").value;
        correctMaxSize = floatList.GetFloatVar("Max Correct").value;
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

    public void Shrink(float resizeSpeed)
    {
        if (currentSize > minSize)
        {
            currentSize -= resizeSpeed * Time.deltaTime;
            transform.localScale = defaultScale * currentSize;
        }
        IsCorrect();
    }

    public void Enlarge(float resizeSpeed)
    {
        if (currentSize < maxSize)
        {
            currentSize += resizeSpeed * Time.deltaTime;
            transform.localScale = defaultScale * currentSize;
        }
        IsCorrect();
    }

    void IsCorrect()
    {
        if (currentSize >= correctMinSize && currentSize <= correctMaxSize && !correct)
        {
            correct = true;
            correctSize.Invoke();
        }
        else if (correct && (currentSize < correctMinSize || currentSize > correctMaxSize))
        {
            correct = false;
            correctSize.Invoke();
        }
    }
}
