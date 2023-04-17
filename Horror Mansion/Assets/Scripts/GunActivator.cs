using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunActivator : MonoBehaviour
{
    [SerializeField] GameEvent battery;
    [SerializeField] GameEvent keyCard;
    [SerializeField] GameEvent gunEnabler;
    int counter;

    // Start is called before the first frame update
    void Start()
    {
        battery.Subscribe(Checker);
        keyCard.Subscribe(Checker);
    }

    void Checker()
    {
        counter++;

        if (counter == 2)
        {
            gunEnabler.Notify();
        }
    }
}
