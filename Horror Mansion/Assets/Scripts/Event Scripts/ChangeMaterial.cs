using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{
    [SerializeField] GameEvent incoming;
    [SerializeField] Material switched;
    Renderer myrender;
    // Start is called before the first frame update
    void Start()
    {
        myrender = GetComponent<Renderer>();
        incoming.Subscribe(SwitchMaterial);
    }

   void SwitchMaterial()
    {
        myrender.material = switched;
        incoming.Unsubscribe(SwitchMaterial);
    }
}
