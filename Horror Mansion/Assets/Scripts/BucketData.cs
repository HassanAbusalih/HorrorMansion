using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
