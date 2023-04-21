using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script handles moving the player using Unity's character controller.
/// A float list is used to get the player's speed, sprint modifier and gravity.
/// Since this uses an unchanging value for the gravity, the player will always fall at the same speed.
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    CharacterController controller;
    Vector3 direction = Vector3.zero;
    [SerializeField] FloatList floatList;
    float speed;
    float sprintModifier;
    float gravity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        speed = floatList.GetFloatVar("Speed").value;
        sprintModifier = floatList.GetFloatVar("Sprint Modifier").value;
        gravity = floatList.GetFloatVar("Gravity").value;
    }

    void FixedUpdate()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        direction = new Vector3(horizontal, 0, vertical);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            direction = transform.TransformDirection(direction.normalized) * speed * sprintModifier;
        }
        else
        {
            direction = transform.TransformDirection(direction.normalized) * speed;
        }
        if (!controller.isGrounded)
        {
            direction.y -= gravity;
        }
        controller.Move(direction * Time.deltaTime);
    }
}
