using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketManager : MonoBehaviour
{
    [SerializeField] GameEvent incoming;
    [SerializeField] GameEvent outgoing;
    BucketData[] buckets;
    BucketData addingBucket;
    BucketData receivingBucket;

    private void Start()
    {
        buckets = FindObjectsOfType<BucketData>();
        incoming.SubscribeObj(ChooseBucket);
    }

    public void ChooseBucket(object bucket)
    {
        BucketData newBucket = (BucketData)bucket;
        if (addingBucket != null && newBucket != addingBucket)
        {
            receivingBucket = newBucket;
            AddToBucket();
        }
        else
        {
            addingBucket = newBucket;
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
        outgoing.Notify();
    }

}
