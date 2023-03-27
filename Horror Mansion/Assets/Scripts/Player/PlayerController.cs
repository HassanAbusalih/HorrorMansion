using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour
{
    CharacterController controller;
    Vector3 direction = Vector3.zero;
    [SerializeField] FloatList floatList;

    void Start()
    {
        controller = GetComponent<CharacterController>();    
    }

    void FixedUpdate()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        direction = new Vector3(horizontal, 0, vertical);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            direction = transform.TransformDirection(direction.normalized) * floatList.GetFloatVar("Speed").value * floatList.GetFloatVar("Sprint Modifier").value;
        }
        else
        {
            direction = transform.TransformDirection(direction.normalized) * floatList.GetFloatVar("Speed").value;
        }
        if (!controller.isGrounded)
        {
            direction.y -= floatList.GetFloatVar("Gravity").value;
        }
        controller.Move(direction * Time.deltaTime);
    }
}
