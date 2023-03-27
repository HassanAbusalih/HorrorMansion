using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SmoothResizable : MonoBehaviour
{
    [SerializeField] bool allowPickUpWhenCorrect;
    [SerializeField] float maxSize;
    [SerializeField] float minSize;
    [SerializeField] float correctMinSize;
    [SerializeField] float correctMaxSize;
    UnityEvent correctSize = new();
    Vector3 defaultScale;
    float currentSize = 1;
    bool correct;

    void Start()
    {
        defaultScale = transform.localScale;
        if (allowPickUpWhenCorrect)
        {
            PickUpable pickUpable = GetComponent<PickUpable>();
            if (pickUpable == null)
            {
                pickUpable = gameObject.AddComponent<PickUpable>();
            }
            pickUpable.SetEvent(correctSize, name);
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
