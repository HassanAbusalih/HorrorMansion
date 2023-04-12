using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowObject : MonoBehaviour
{
    [SerializeField] GameEvent gameEvent;

    private void Start()
    {
        gameEvent.Subscribe(ToggleObject);
    }

    void ToggleObject()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

}
