using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// - Allows an object to be smoothly resized
/// - Min and max size need to be given, as well as min and max for when the object can be picked up
/// </summary>
public class SmoothResizable : Resizable
{
    [SerializeField] bool allowPickUpWhenCorrect;
    [SerializeField] float maxSize;
    [SerializeField] float minSize;
    [SerializeField] float pickUpMinSize;
    [SerializeField] float pickUpMaxSize;
    UnityEvent correctSize = new();
    Vector3 defaultScale;
    bool correct;
    float sizePercentage;

    void Start()
    {
        defaultScale = transform.localScale;
        if (allowPickUpWhenCorrect)
        {
            Interactable interactable = GetComponent<Interactable>();
            if (interactable == null)
            {
                interactable = gameObject.AddComponent<Interactable>();
            }
            if (currentSize >= pickUpMinSize && currentSize <= pickUpMaxSize && !correct)
            {
                correct = true;
            }
            interactable.SetEvent(correctSize, correct);
        }
    }

    public float Shrink(float resizeSpeed)
    {
        if (currentSize > minSize)
        {
            currentSize -= resizeSpeed * Time.deltaTime;
            transform.localScale = defaultScale * currentSize;
        }
        IsCorrect();
        sizePercentage = (currentSize - minSize) / (maxSize - minSize);
        return sizePercentage;
    }

    public float Enlarge(float resizeSpeed)
    {
        if (currentSize < maxSize)
        {
            currentSize += resizeSpeed * Time.deltaTime;
            transform.localScale = defaultScale * currentSize;
        }
        IsCorrect();
        sizePercentage = (currentSize - minSize) / (maxSize - minSize);
        return sizePercentage;
    }

    void IsCorrect()
    {
        if (currentSize >= pickUpMinSize && currentSize <= pickUpMaxSize && !correct)
        {
            correct = true;
            correctSize?.Invoke();
        }
        else if (correct && (currentSize < pickUpMinSize || currentSize > pickUpMaxSize))
        {
            correct = false;
            correctSize?.Invoke();
        }
    }
}
