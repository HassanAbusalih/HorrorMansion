using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizePuzzle : MonoBehaviour
{

    public GameEvent gameEvent;
    // Start is called before the first frame update
    void Start()
    {
        gameEvent.Subscribe(Solve);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Solve()
    {
        gameObject.SetActive(false);
    }
}
