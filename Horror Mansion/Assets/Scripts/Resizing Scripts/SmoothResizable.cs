using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// - Allows an object to be gradually resized
/// - Min and max size need to be given, as well as min and max for when the object can be picked up
/// - Sets up an event to align whether it is within the pickup range with the interactable component attached to the same object.
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
            if (!TryGetComponent<Interactable>(out var interactable))
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

    /// <summary>
    /// If the object is larger than its minimum size, this reduces its size. It then checks if the correct size is reached, which is the range within which it can be picked up.
    /// </summary>
    /// <param name="resizeSpeed"> The speed used to resize the object. </param>
    /// <returns> The current size percentage of the object. Min is 0, max is 1. </returns>

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

    /// <summary>
    /// If the object is smaller than its maximum size, this increases its size. It then checks if the correct size is reached, which is the range within which it can be picked up.
    /// </summary>
    /// <param name="resizeSpeed"> The speed used to resize the object. </param>
    /// <returns> The current size percentage of the object. Min is 0, max is 1. </returns>

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
