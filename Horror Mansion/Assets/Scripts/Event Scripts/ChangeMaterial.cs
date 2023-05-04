using UnityEngine;

/// <summary>
/// This script changes the material of the object it is attached to when the GameEvent it is subscribed to is notified.
/// </summary>

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
