using UnityEngine;

/// <summary>
/// Receives BucketData using an incoming GameEvent, and once it has received two buckets, attempts to pour from the first bucket received to the second and updates their images with their fill amount. 
/// It also has an array of BucketData which it finds using FindObjectsOfType. Once all buckets in the array are at their desired amount, the outgoing GameEvent is notified.
/// </summary>

public class BucketManager : MonoBehaviour, INotifier, ISubscriber
{
    [SerializeField] GameEvent incoming;
    [SerializeField] GameEvent outgoing;
    public GameEvent Subscriber => incoming;
    public GameEvent Notifier => outgoing;
    string ISubscriber.GetName() => nameof(incoming);
    string INotifier.GetName() => nameof(outgoing);
    BucketData[] buckets;
    BucketData addingBucket;
    BucketData receivingBucket;

    private void Start()
    {
        buckets = FindObjectsOfType<BucketData>();
        incoming.SubscribeObj(ChooseBucket);
    }

    void ChooseBucket(object bucket)
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
        incoming.UnsubscribeObj(ChooseBucket);
    }
}
