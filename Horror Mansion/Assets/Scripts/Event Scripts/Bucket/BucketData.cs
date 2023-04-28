using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Holds all data relating to a 'bucket' (its current, max and desired amounts), and changes the fill amount of its referenced image on start.
/// </summary>

public class BucketData : MonoBehaviour
{
    public int currentAmount;
    public int maxAmount;
    public int desiredAmount;
    public Image image;

    private void Start()
    {
        image.fillAmount = (float)currentAmount / maxAmount;
    }
}
