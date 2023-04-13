using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterBucket : MonoBehaviour
{
    [SerializeField] GameEvent gameEvent;
    BucketData[] buckets;
    BucketData addingBucket;
    BucketData receivingBucket;

    private void Start()
    {
        buckets = FindObjectsOfType<BucketData>();
    }

    public void ChooseBucket(BucketData bucket)
    {
        if (addingBucket != null && bucket != addingBucket)
        {
            receivingBucket = bucket;
            AddToBucket();
        }
        else
        {
            addingBucket = bucket;
        }
    }

    void AddToBucket()
    {
        int spaceInBucket = receivingBucket.maxAmount - receivingBucket.currentAmount;
        int amountToAdd = Mathf.Min(spaceInBucket, addingBucket.currentAmount);
        receivingBucket.currentAmount += amountToAdd;
        addingBucket.currentAmount -= amountToAdd;
        addingBucket.image.fillAmount = (float)addingBucket.currentAmount / addingBucket.maxAmount;
        receivingBucket.image.fillAmount = (float)receivingBucket.currentAmount / receivingBucket.maxAmount;
        CheckIfSolved();
    }

    void CheckIfSolved()
    {
        addingBucket = null;
        receivingBucket = null;
        foreach(BucketData bucket in buckets)
        {
            if (bucket.currentAmount != bucket.desiredAmount)
            {
                return;
            }
        }
        gameEvent.Notify();
    }

}
