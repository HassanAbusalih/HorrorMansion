using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterBucket : MonoBehaviour
{
    [SerializeField] Bucket[] buckets;

    void Start()
    {
        for (int i = 0; i < buckets.Length; i++)
        {
            buckets[i].image.fillAmount = (float)buckets[i].currentAmount / buckets[i].maxAmount;
            buckets[i].index = i;
        }
    }

    public void AddToBucket(int addingIndex, int receivingIndex)
    {
        if (addingIndex >= buckets.Length || receivingIndex >= buckets.Length)
        {
            Debug.Log("Invalid bucket index!");
            return;
        }
        Bucket addingBucket = buckets[addingIndex];
        Bucket receivingBucket = buckets[receivingIndex];
        int spaceInBucket = receivingBucket.maxAmount - receivingBucket.currentAmount;
        int amountToAdd = Mathf.Min(spaceInBucket, addingBucket.currentAmount);
        receivingBucket.currentAmount += amountToAdd;
        addingBucket.currentAmount -= amountToAdd;
        addingBucket.image.fillAmount = (float)addingBucket.currentAmount / addingBucket.maxAmount;
        receivingBucket.image.fillAmount = (float)receivingBucket.currentAmount / receivingBucket.maxAmount;
    }
}

public class Bucket
{
    [HideInInspector] public int index;
    public int currentAmount;
    public int maxAmount;
    public Image image;
}