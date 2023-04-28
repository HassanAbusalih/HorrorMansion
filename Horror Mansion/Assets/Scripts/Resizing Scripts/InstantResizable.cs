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

/// <summary>
/// This was created specifically because ResizedObjectReceiver was being annoying since I had to account for two kinds of resizables. 
/// InstantResizable is unused so it ended up not mattering. Fun!
/// </summary>
public class Resizable : MonoBehaviour
{
    [HideInInspector] public float currentSize = 1;
}